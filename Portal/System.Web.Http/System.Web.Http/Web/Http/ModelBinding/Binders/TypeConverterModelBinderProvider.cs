using System;
using System.Web.Http.Internal;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000079 RID: 121
	public sealed class TypeConverterModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06000325 RID: 805 RVA: 0x000091DE File Offset: 0x000073DE
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			if (!TypeHelper.HasStringConverter(modelType))
			{
				return null;
			}
			return new TypeConverterModelBinder();
		}
	}
}
