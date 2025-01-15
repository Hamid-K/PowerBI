using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x0200010E RID: 270
	internal sealed class ODataValueMaterializer : ODataMessageReaderMaterializer
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x0002975F File Offset: 0x0002795F
		public ODataValueMaterializer(ODataMessageReader reader, IODataMaterializerContext materializerContext, Type expectedType, bool? singleResult)
			: base(reader, materializerContext, expectedType, singleResult)
		{
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002B60C File Offset: 0x0002980C
		internal override object CurrentValue
		{
			get
			{
				return this.currentValue;
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002B614 File Offset: 0x00029814
		protected override void ReadWithExpectedType(IEdmTypeReference expectedClientType, IEdmTypeReference expectedReaderType)
		{
			object obj = this.messageReader.ReadValue(expectedReaderType);
			this.currentValue = base.PrimitiveValueMaterializier.MaterializePrimitiveDataValue(base.ExpectedType, null, obj);
		}

		// Token: 0x04000642 RID: 1602
		private object currentValue;
	}
}
