using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StocksMobile.Services
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        const string ResourceId = "StocksMobile.Resources.AppResources";

        static readonly ResourceManager resmgr = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

        public string Text { get; set; }

        private static TranslateExtension instance;

        static TranslateExtension()
        {
            instance = new TranslateExtension();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            //var ci = CrossMultilingual.Current.CurrentCultureInfo;
            //var translation = resmgr.GetString(Text, ci);
            string s = $"{Text.ToUpper()}_{CurrentUserManager.CurrentUser?.Language.ToUpper() ?? "EN"}";
            var translation = resmgr.GetString(s);
            
            if (translation == null)
            {
                translation = Text;
            }
            return translation;
        }

        public static string Translate(string str)
        {
            instance.Text = str;
            return (string)instance.ProvideValue(null);
        }
    }
}
