using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004FD RID: 1277
	internal class SapBwVariableMasterTableProvider : SapBwVariableValueProvider
	{
		// Token: 0x060029A5 RID: 10661 RVA: 0x0007C7E3 File Offset: 0x0007A9E3
		public SapBwVariableMasterTableProvider(ISapBwService service, SapBwMdxCube mdxCube, SapBwVariable variable, bool allowNonAssigned)
			: base(service, mdxCube, variable, allowNonAssigned)
		{
			variable.EnsureInfoObjectDetails(service);
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x00002139 File Offset: 0x00000339
		public override bool HasValues
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x0007C7F8 File Offset: 0x0007A9F8
		public override IEnumerable<IValueReference> GetValues()
		{
			if (this.cachedValues == null)
			{
				this.cachedValues = new List<IValueReference>();
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				if (base.Variable.MasterDataTable != null && base.Service.TryExtractTable("Parameters/MembersFromMasterTable", this.GetMasterTableSelect(), out dataReaderWithTableSchema))
				{
					using (dataReaderWithTableSchema)
					{
						while (dataReaderWithTableSchema.Read())
						{
							string @string = dataReaderWithTableSchema.GetString(0);
							if (!string.IsNullOrEmpty(@string))
							{
								string text = ((base.Variable.CaptionSource == null || string.IsNullOrEmpty(dataReaderWithTableSchema.GetString(1))) ? @string : dataReaderWithTableSchema.GetString(1));
								Value value = TextValue.New(SapBwIdentifier.TrimAndQuotePart(@string, base.Variable.InternalLength));
								value = NavigationTableServices.AddCaption(value, text);
								this.cachedValues.Add(value);
							}
						}
					}
				}
			}
			return this.cachedValues;
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x0007C8D8 File Offset: 0x0007AAD8
		private SapBwMetadataAstCreator GetMasterTableSelect()
		{
			SapBwMetadataAstCreator sapBwMetadataAstCreator = new SapBwMetadataAstCreator(new long?(2500L));
			if (base.Variable.MasterDataTable.StartsWith("/BIC", StringComparison.Ordinal))
			{
				sapBwMetadataAstCreator.AddSelectColumns(new string[] { "/BIC/" + base.Variable.InfoObject });
			}
			else
			{
				sapBwMetadataAstCreator.AddSelectColumns(new string[] { base.Variable.InfoObject.Substring(1) });
			}
			sapBwMetadataAstCreator.AddTable(base.Variable.MasterDataTable);
			if (base.Variable.CaptionSource != null)
			{
				sapBwMetadataAstCreator.AddSelectColumns(new string[] { base.Variable.CaptionSource });
			}
			if (base.Variable.IsLanguageDependent && base.Variable.CaptionSource != null)
			{
				sapBwMetadataAstCreator.AddCondition("LANGU", TextValue.New(base.Service.Language));
			}
			return sapBwMetadataAstCreator;
		}

		// Token: 0x04001214 RID: 4628
		private const int identifierOrdinal = 0;

		// Token: 0x04001215 RID: 4629
		private const int captionOrdinal = 1;

		// Token: 0x04001216 RID: 4630
		private List<IValueReference> cachedValues;
	}
}
