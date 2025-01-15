using System;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001E6 RID: 486
	internal sealed class EdmConceptualMeasure : EdmConceptualProperty, IConceptualMeasure, IConceptualProperty, IConceptualDisplayItem, IEquatable<IConceptualProperty>
	{
		// Token: 0x06001720 RID: 5920 RVA: 0x0003F854 File Offset: 0x0003DA54
		internal EdmConceptualMeasure(EdmProperty property)
			: base(property)
		{
			this._edmMeasure = property as EdmMeasure;
			this._kpi = ((this._edmMeasure != null && this._edmMeasure.Kpi != null) ? new EdmConceptualKpi(this._edmMeasure.Kpi) : null);
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0003F8A2 File Offset: 0x0003DAA2
		public IConceptualKpi Kpi
		{
			get
			{
				return this._kpi;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x0003F8AA File Offset: 0x0003DAAA
		public IConceptualMeasure DynamicFormatString
		{
			get
			{
				return this._dynamicFormatString;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0003F8B2 File Offset: 0x0003DAB2
		public IConceptualMeasure DynamicFormatCulture
		{
			get
			{
				return this._dynamicFormatCulture;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0003F8BC File Offset: 0x0003DABC
		public bool IsVariant
		{
			get
			{
				return this._edmMeasure.ActualType.Equals(ActualDataType.Any);
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0003F8E8 File Offset: 0x0003DAE8
		public ConceptualMeasureTemplate Template
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0003F8EF File Offset: 0x0003DAEF
		public ConceptualDataChangeDetectionMetadata ChangeDetectionMetadata
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0003F8F6 File Offset: 0x0003DAF6
		public ConceptualDistributiveAggregateKind? DistributiveAggegate
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x0003F8FD File Offset: 0x0003DAFD
		public IReadOnlyList<IConceptualEntity> DistributiveBy
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0003F904 File Offset: 0x0003DB04
		internal override void CompleteInitialization(EdmConceptualPropertyInitContext context)
		{
			base.CompleteInitialization(context);
			if (this._kpi != null)
			{
				this._kpi.CompleteInitialization(context);
			}
			if (this._edmMeasure != null)
			{
				this._dynamicFormatString = this.CompleteDynamicFormatting(context, this._edmMeasure, this._edmMeasure.FormatBy);
				this._dynamicFormatCulture = this.CompleteDynamicFormatting(context, this._edmMeasure, this._edmMeasure.ApplyCulture);
			}
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0003F970 File Offset: 0x0003DB70
		private EdmConceptualMeasure CompleteDynamicFormatting(EdmConceptualPropertyInitContext context, EdmMeasure dynamicallyFormattedMeasure, EdmMeasure dynamicFormattingMeasure)
		{
			if (dynamicFormattingMeasure == null)
			{
				return null;
			}
			if (dynamicFormattingMeasure.FormatBy != null || dynamicFormattingMeasure.ApplyCulture != null || dynamicallyFormattedMeasure == dynamicFormattingMeasure)
			{
				return null;
			}
			return context.GetProperty<EdmConceptualMeasure>(dynamicFormattingMeasure);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0003F994 File Offset: 0x0003DB94
		public override string ToString()
		{
			IConceptualEntity entity = base.Entity;
			string text;
			if (entity == null)
			{
				text = null;
			}
			else
			{
				IConceptualSchema schema = entity.Schema;
				text = ((schema != null) ? schema.SchemaId : null);
			}
			string text2 = text ?? string.Empty;
			if (!string.IsNullOrEmpty(text2))
			{
				text2 = "'" + text2 + "'.";
			}
			string[] array = new string[7];
			array[0] = "EdmConceptualMeasure : ";
			array[1] = text2;
			array[2] = "'";
			int num = 3;
			IConceptualEntity entity2 = base.Entity;
			array[num] = ((entity2 != null) ? entity2.Name : null);
			array[4] = "'[";
			array[5] = base.Name;
			array[6] = "]";
			return string.Concat(array);
		}

		// Token: 0x04000C59 RID: 3161
		private readonly EdmConceptualKpi _kpi;

		// Token: 0x04000C5A RID: 3162
		private readonly EdmMeasure _edmMeasure;

		// Token: 0x04000C5B RID: 3163
		private EdmConceptualMeasure _dynamicFormatString;

		// Token: 0x04000C5C RID: 3164
		private EdmConceptualMeasure _dynamicFormatCulture;
	}
}
