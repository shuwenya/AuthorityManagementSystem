using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace AMS.Util
{
    public class ModelBindingMetadataProvider : IMetadataDetailsProvider, IDisplayMetadataProvider
    {
        /// <summary>
        /// Controller Model Binding 处理
        /// </summary>
        /// <param name="context"></param>
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            if (context.Key.MetadataKind == ModelMetadataKind.Property)
            {
                context.DisplayMetadata.ConvertEmptyStringToNull = false;
            }
        }
    }
}
