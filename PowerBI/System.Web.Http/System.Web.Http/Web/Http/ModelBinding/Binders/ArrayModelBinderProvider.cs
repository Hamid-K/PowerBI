using System;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000069 RID: 105
	public sealed class ArrayModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x060002DF RID: 735 RVA: 0x00008398 File Offset: 0x00006598
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			if (!modelType.IsArray)
			{
				return null;
			}
			Type elementType = modelType.GetElementType();
			return (IModelBinder)Activator.CreateInstance(typeof(ArrayModelBinder<>).MakeGenericType(new Type[] { elementType }));
		}
	}
}
