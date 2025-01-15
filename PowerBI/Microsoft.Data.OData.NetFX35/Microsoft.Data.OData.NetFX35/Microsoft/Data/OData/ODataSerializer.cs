using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020000EE RID: 238
	internal abstract class ODataSerializer
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x0001504D File Offset: 0x0001324D
		protected ODataSerializer(ODataOutputContext outputContext)
		{
			this.outputContext = outputContext;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001505C File Offset: 0x0001325C
		internal bool UseClientFormatBehavior
		{
			get
			{
				return this.outputContext.UseClientFormatBehavior;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00015069 File Offset: 0x00013269
		internal bool UseServerFormatBehavior
		{
			get
			{
				return this.outputContext.UseServerFormatBehavior;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00015076 File Offset: 0x00013276
		internal bool UseDefaultFormatBehavior
		{
			get
			{
				return this.outputContext.UseDefaultFormatBehavior;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00015083 File Offset: 0x00013283
		internal ODataMessageWriterSettings MessageWriterSettings
		{
			get
			{
				return this.outputContext.MessageWriterSettings;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00015090 File Offset: 0x00013290
		internal IODataUrlResolver UrlResolver
		{
			get
			{
				return this.outputContext.UrlResolver;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001509D File Offset: 0x0001329D
		internal ODataVersion Version
		{
			get
			{
				return this.outputContext.Version;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x000150AA File Offset: 0x000132AA
		internal bool WritingResponse
		{
			get
			{
				return this.outputContext.WritingResponse;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x000150B7 File Offset: 0x000132B7
		internal IEdmModel Model
		{
			get
			{
				return this.outputContext.Model;
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000150C4 File Offset: 0x000132C4
		internal DuplicatePropertyNamesChecker CreateDuplicatePropertyNamesChecker()
		{
			return new DuplicatePropertyNamesChecker(this.MessageWriterSettings.WriterBehavior.AllowDuplicatePropertyNames, this.WritingResponse);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000150E1 File Offset: 0x000132E1
		protected void ValidateAssociationLink(ODataAssociationLink associationLink, IEdmEntityType entryEntityType)
		{
			WriterValidationUtils.ValidateAssociationLink(associationLink, this.Version, this.WritingResponse);
			WriterValidationUtils.ValidateNavigationPropertyDefined(associationLink.Name, entryEntityType, this.outputContext.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
		}

		// Token: 0x04000271 RID: 625
		private readonly ODataOutputContext outputContext;
	}
}
