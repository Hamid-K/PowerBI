using System;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Internal;
using System.Data.Entity.ModelConfiguration.Edm.Serialization;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Xml;
using System.Xml.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000241 RID: 577
	public static class EdmxWriter
	{
		// Token: 0x06001E40 RID: 7744 RVA: 0x0005463C File Offset: 0x0005283C
		public static void WriteEdmx(DbContext context, XmlWriter writer)
		{
			Check.NotNull<DbContext>(context, "context");
			Check.NotNull<XmlWriter>(writer, "writer");
			InternalContext internalContext = context.InternalContext;
			if (internalContext is EagerInternalContext)
			{
				throw Error.EdmxWriter_EdmxFromObjectContextNotSupported();
			}
			DbModel modelBeingInitialized = internalContext.ModelBeingInitialized;
			if (modelBeingInitialized != null)
			{
				EdmxWriter.WriteEdmx(modelBeingInitialized, writer);
				return;
			}
			DbCompiledModel codeFirstModel = internalContext.CodeFirstModel;
			if (codeFirstModel == null)
			{
				throw Error.EdmxWriter_EdmxFromModelFirstNotSupported();
			}
			DbModelStore service = DbConfiguration.DependencyResolver.GetService<DbModelStore>();
			if (service != null)
			{
				XDocument xdocument = service.TryGetEdmx(context.GetType());
				if (xdocument != null)
				{
					xdocument.WriteTo(writer);
					return;
				}
			}
			DbModelBuilder cachedModelBuilder = codeFirstModel.CachedModelBuilder;
			if (cachedModelBuilder == null)
			{
				throw Error.EdmxWriter_EdmxFromRawCompiledModelNotSupported();
			}
			DbModelBuilder dbModelBuilder = cachedModelBuilder.Clone();
			EdmxWriter.WriteEdmx((internalContext.ModelProviderInfo == null) ? dbModelBuilder.Build(internalContext.Connection) : dbModelBuilder.Build(internalContext.ModelProviderInfo), writer);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00054701 File Offset: 0x00052901
		public static void WriteEdmx(DbModel model, XmlWriter writer)
		{
			Check.NotNull<DbModel>(model, "model");
			Check.NotNull<XmlWriter>(writer, "writer");
			new EdmxSerializer().Serialize(model.DatabaseMapping, writer);
		}
	}
}
