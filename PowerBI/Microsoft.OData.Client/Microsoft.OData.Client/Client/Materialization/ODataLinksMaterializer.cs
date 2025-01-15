using System;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x02000109 RID: 265
	internal sealed class ODataLinksMaterializer : ODataMessageReaderMaterializer
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0002975F File Offset: 0x0002795F
		public ODataLinksMaterializer(ODataMessageReader reader, IODataMaterializerContext materializerContext, Type expectedType, bool? singleResult)
			: base(reader, materializerContext, expectedType, singleResult)
		{
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0002AC10 File Offset: 0x00028E10
		internal override long CountValue
		{
			get
			{
				if (this.links == null && !this.IsDisposed)
				{
					this.ReadLinks();
				}
				if (this.links != null && this.links.Count != null)
				{
					return this.links.Count.Value;
				}
				throw new InvalidOperationException(Strings.MaterializeFromAtom_CountNotPresent);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00003487 File Offset: 0x00001687
		internal override object CurrentValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00004A70 File Offset: 0x00002C70
		internal override bool IsCountable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002AC70 File Offset: 0x00028E70
		protected override void ReadWithExpectedType(IEdmTypeReference expectedClientType, IEdmTypeReference expectedReaderType)
		{
			this.ReadLinks();
			Type type = Nullable.GetUnderlyingType(base.ExpectedType) ?? base.ExpectedType;
			ClientEdmModel model = base.MaterializerContext.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(type));
			if (clientTypeAnnotation.IsEntityType)
			{
				throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidEntityType(clientTypeAnnotation.ElementTypeName));
			}
			throw Error.InvalidOperation(Strings.Deserialize_MixedTextWithComment);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002ACD8 File Offset: 0x00028ED8
		private void ReadLinks()
		{
			try
			{
				if (this.links == null)
				{
					this.links = this.messageReader.ReadEntityReferenceLinks();
				}
			}
			catch (ODataErrorException ex)
			{
				throw new DataServiceClientException(Strings.Deserialize_ServerException(ex.Error.Message), ex);
			}
			catch (ODataException ex2)
			{
				throw new InvalidOperationException(ex2.Message, ex2);
			}
		}

		// Token: 0x04000630 RID: 1584
		private ODataEntityReferenceLinks links;
	}
}
