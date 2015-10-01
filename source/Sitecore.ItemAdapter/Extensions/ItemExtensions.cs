using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Model;
using Sitecore.Pipelines;
using Sitecore.Pipelines.RenderField;
using Sitecore.Web.UI.WebControls;

namespace Sitecore.ItemAdapter.Extensions
{
    public static class ItemExtensions
    {
        public static bool MatchTemplate(this TemplateItem ti, string TemplateName, bool matchBaseTemplate)
        {
            if (String.Equals(ti.Name, TemplateName, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            else
            {
                if (matchBaseTemplate)
                {
                    TemplateItem[] baseTemplates = ti.BaseTemplates;
                    foreach (TemplateItem bt in baseTemplates)
                    {
                        if (MatchTemplate(bt, TemplateName, matchBaseTemplate))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public static bool MatchTemplateByGuid(this TemplateItem ti, string TemplateGuid, bool matchBaseTemplate)
        {
            if (String.Equals(ti.ID.ToString(), TemplateGuid, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            else
            {
                if (matchBaseTemplate)
                {
                    TemplateItem[] baseTemplates = ti.BaseTemplates;
                    foreach (TemplateItem bt in baseTemplates)
                    {
                        if (MatchTemplate(bt, TemplateGuid, matchBaseTemplate))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }


        static public Field GetField(this Item item, string fieldName)
        {
            return item.Fields[fieldName];
        }
        static public Field GetField(this Item item, ID fieldId)
        {
            return item.Fields[fieldId];
        }
        static public ID GetFieldId(this Item item, string fieldName)
        {
            var field = item.Template.GetField(fieldName);
            if (field != null)
            {
                return field.ID;
            }
            else
            {
                return ID.Null;
            }
        }
        static public string GetFieldName(this Item item, ID fieldId)
        {
            var field = item.Template.GetField(fieldId);
            if (field != null)
            {
                return field.Name;
            }
            else
            {
                return null;
            }
        }



        static public string GetLink(this Item item)
        {
            return Sitecore.Links.LinkManager.GetItemUrl(item);
        }

        static public string GetLink(this Item item, Sitecore.Links.UrlOptions options)
        {
            return Sitecore.Links.LinkManager.GetItemUrl(item, options);
        }

        static public int GetIntegerFieldValue(this Item item, ID fieldId)
        {
            Field f = (Field)GetField(item, fieldId);
            int i = 0;
            Int32.TryParse(f.Value, out i);
            return i;
        }

        static public Decimal GetDecimalFieldValue(this Item item, ID fieldId)
        {
            Field f = (Field)GetField(item, fieldId);
            Decimal d = 0;
            Decimal.TryParse(f.Value, out d);
            return d;
        }


        static public bool GetCheckBoxFieldValue(this Item item, ID fieldId)
        {
            CheckboxField f = (CheckboxField)GetField(item, fieldId);
            if (f != null)
            {
                return f.Checked;
            }
            else
            {
                return false;
            }
        }

        static public string GetTextContent(this Item item, ID fieldId)
        {
            Field f = GetField(item, fieldId);
            if (f != null)
            {
                return f.Value;
            }
            else
            {
                return null;
            }
        }

        static public string GetRichTextContent(this Item item, ID fieldId)
        {
            return GetRichTextRenderFieldPipeline(item, fieldId);
        }

        static public string GetRichTextRenderFieldPipeline(this Item item, ID fieldId)
        {
            RenderFieldArgs args = new RenderFieldArgs();
            args.Item = item;
            args.DisableWebEditFieldWrapping = true;
            args.FieldName = GetFieldName(item, fieldId);
            CorePipeline.Run("renderField", args);
            return args.Result.ToString();
        }

        static public Nullable<DateTime> GetDateTimeFieldValue(this Item item, ID fieldId)
        {
            DateField DateField = (DateField)GetField(item, fieldId);
            if (DateField != null)
            {
                return DateField.DateTime;
            }
            else
            {
                return null;
            }
        }

        static public GeneralLink GetGeneralLinkFieldValue(this Item item, ID fieldId)
        {
            LinkField f = ((LinkField)GetField(item, fieldId));
            if (f != null && f.IsInternal && f.TargetItem != null)
            {
                return new GeneralLink()
                {
                    Url = f.TargetItem.GetLink(),
                    Text = f.Text,
                    CssClass = f.Class,
                    Target = f.Target
                };
            }
            else if (f != null)
            {
                return new GeneralLink()
                {
                    Url = f.Url,
                    Text = f.Text,
                    CssClass = f.Class,
                    Target = f.Target
                };
            }
            else
            {
                return null;
            }
        }

        static public string GetImageUrl(this Item item, ID fieldId)
        {
            string url = "";

            ImageField theImage = (ImageField)GetField(item, fieldId);
            if (theImage.MediaItem != null)
            {
                url = Sitecore.Resources.Media.MediaManager.GetMediaUrl(theImage.MediaItem);
                url = Sitecore.StringUtil.EnsurePrefix('/', url);
            }

            return url;
        }

        static public string GetFileUrl(this Item item, ID fieldId)
        {
            string url = "";

            FileField theFile = (FileField)GetField(item, fieldId);
            if (theFile != null && theFile.MediaItem != null)
            {
                url = Sitecore.Resources.Media.MediaManager.GetMediaUrl(theFile.MediaItem);
                url = Sitecore.StringUtil.EnsurePrefix('/', url);
            }

            return url;
        }

        static public List<string> GetTreelistMediaUrls(this Item item, ID fieldId)
        {
            List<string> l = new List<string>();
            MultilistField theField = (MultilistField)GetField(item, fieldId);
            if (theField != null)
            {
                Item[] LinkedItems = theField.GetItems();
                foreach (Item i in LinkedItems)
                {
                    if (i.TemplateName == "Media Folder")
                    {
                    }
                    else
                    {
                        string url = String.Empty;
                        url = Sitecore.Resources.Media.MediaManager.GetMediaUrl(new MediaItem(i));
                        url = Sitecore.StringUtil.EnsurePrefix('/', url);
                        l.Add(url);
                    }
                }
            }
            return l;
        }

        static public Item GetInternalLinkItem(this Item item, ID fieldId)
        {
            Sitecore.Data.Fields.InternalLinkField ilf = GetField(item, fieldId);

            if (ilf != null && ilf.TargetItem != null)
                return ilf.TargetItem;
            else
                return null;
        }

        static public string GetInternalLinkId(this Item item, ID fieldId)
        {
            Sitecore.Data.Fields.InternalLinkField ilf = GetField(item, fieldId);

            if (ilf != null && ilf.TargetItem != null)
                return ilf.TargetItem.ID.ToString();
            else
                return "";
        }

        static public Item GetDropLinkItem(this Item item, ID fieldId)
        {
            Sitecore.Data.Fields.LookupField ilf = GetField(item, fieldId);

            if (ilf != null && ilf.TargetItem != null)
                return ilf.TargetItem;
            else
                return null;
        }

        static public List<Item> GetListItems(this Item item, ID fieldId)
        {
            Sitecore.Data.Fields.MultilistField mlf = GetField(item, fieldId);

            if (mlf != null && mlf.Count > 0)
                return mlf.GetItems().ToList();
            else
                return null;
        }

        static public SortedList<string, string> GetNameValueList(this Item item, ID fieldId)
        {
            SortedList<string, string> dict = new SortedList<string, string>();
            Field field = item.Fields[fieldId];

            if (field != null && !string.IsNullOrEmpty(field.Value))
            {
                foreach (string v in field.Value.Split('&'))
                {
                    if (!string.IsNullOrEmpty(v))
                    {
                        string[] kvArray = v.Split('=');
                        if (kvArray.Length == 2)
                        {
                            dict.Add(System.Net.WebUtility.UrlDecode(kvArray[0]), System.Net.WebUtility.UrlDecode(kvArray[1]));
                        }
                    }
                }
            }

            return dict;
        }

        static public SortedList<string, Item> GetNameLookupList(this Item item, ID fieldId)
        {
            SortedList<string, string> nameValueList = GetNameValueList(item, fieldId);
            SortedList<string, Item> result = new SortedList<string, Item>();

            foreach (var kvp in nameValueList)
            {
                Data.ID itemId = Data.ID.Parse(kvp.Value);
                result.Add(kvp.Key, item.Database.GetItem(itemId));
            }
            return result;
        }

    }


}
