using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.JsonLight;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x0200007E RID: 126
	internal abstract class ODataEntityMetadataBuilder
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00012711 File Offset: 0x00010911
		internal static ODataEntityMetadataBuilder Null
		{
			get
			{
				return ODataEntityMetadataBuilder.NullEntityMetadataBuilder.Instance;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00012718 File Offset: 0x00010918
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00012720 File Offset: 0x00010920
		internal ODataEntityMetadataBuilder ParentMetadataBuilder { get; set; }

		// Token: 0x060004F7 RID: 1271
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract Uri GetEditLink();

		// Token: 0x060004F8 RID: 1272
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract Uri GetReadLink();

		// Token: 0x060004F9 RID: 1273
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract Uri GetId();

		// Token: 0x060004FA RID: 1274
		internal abstract bool TryGetIdForSerialization(out Uri id);

		// Token: 0x060004FB RID: 1275
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract string GetETag();

		// Token: 0x060004FC RID: 1276 RVA: 0x00012729 File Offset: 0x00010929
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual ODataStreamReferenceValue GetMediaResource()
		{
			return null;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001273F File Offset: 0x0001093F
		internal virtual IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			if (nonComputedProperties != null)
			{
				return Enumerable.Where<ODataProperty>(nonComputedProperties, (ODataProperty p) => !(p.ODataValue is ODataStreamReferenceValue));
			}
			return null;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00012769 File Offset: 0x00010969
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual IEnumerable<ODataAction> GetActions()
		{
			return null;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001276C File Offset: 0x0001096C
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual IEnumerable<ODataFunction> GetFunctions()
		{
			return null;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001276F File Offset: 0x0001096F
		internal virtual void MarkNavigationLinkProcessed(string navigationPropertyName)
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00012771 File Offset: 0x00010971
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual ODataJsonLightReaderNavigationLinkInfo GetNextUnprocessedNavigationLink()
		{
			return null;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00012774 File Offset: 0x00010974
		internal virtual Uri GetStreamEditLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00012782 File Offset: 0x00010982
		internal virtual Uri GetStreamReadLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00012790 File Offset: 0x00010990
		internal virtual Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNavigationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001279E File Offset: 0x0001099E
		internal virtual Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000127AC File Offset: 0x000109AC
		internal virtual Uri GetOperationTargetUri(string operationName, string bindingParameterTypeName, string parameterNames)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000127BA File Offset: 0x000109BA
		internal virtual string GetOperationTitle(string operationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x0200007F RID: 127
		private sealed class NullEntityMetadataBuilder : ODataEntityMetadataBuilder
		{
			// Token: 0x0600050A RID: 1290 RVA: 0x000127D0 File Offset: 0x000109D0
			private NullEntityMetadataBuilder()
			{
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x000127D8 File Offset: 0x000109D8
			internal override Uri GetEditLink()
			{
				return null;
			}

			// Token: 0x0600050C RID: 1292 RVA: 0x000127DB File Offset: 0x000109DB
			internal override Uri GetReadLink()
			{
				return null;
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x000127DE File Offset: 0x000109DE
			internal override Uri GetId()
			{
				return null;
			}

			// Token: 0x0600050E RID: 1294 RVA: 0x000127E1 File Offset: 0x000109E1
			internal override string GetETag()
			{
				return null;
			}

			// Token: 0x0600050F RID: 1295 RVA: 0x000127E4 File Offset: 0x000109E4
			internal override bool TryGetIdForSerialization(out Uri id)
			{
				id = null;
				return false;
			}

			// Token: 0x04000229 RID: 553
			internal static readonly ODataEntityMetadataBuilder.NullEntityMetadataBuilder Instance = new ODataEntityMetadataBuilder.NullEntityMetadataBuilder();
		}
	}
}
