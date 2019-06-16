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

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            var ci = CrossMultilingual.Current.CurrentCultureInfo;
            var translation = resmgr.GetString(Text, ci);

            if (translation == null)
            {
                translation = Text;
            }
            return translation;
        }
    }
}
