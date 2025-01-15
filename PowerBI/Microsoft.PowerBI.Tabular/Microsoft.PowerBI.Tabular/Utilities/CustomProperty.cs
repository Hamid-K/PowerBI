using System;
using Microsoft.AnalysisServices.Tabular.Json.Linq;

namespace Microsoft.AnalysisServices.Tabular.Utilities
{
	// Token: 0x02000195 RID: 405
	internal struct CustomProperty<TOwner, TMetadataValue, TProperty> where TOwner : MetadataObject where TProperty : class, ICustomProperty<TOwner, TMetadataValue>, new()
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x000A43A8 File Offset: 0x000A25A8
		public TMetadataValue Value
		{
			get
			{
				TMetadataValue tmetadataValue = default(TMetadataValue);
				this.ExtractMetadataValueIfNeeded(ref tmetadataValue, false);
				return tmetadataValue;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x000A43C7 File Offset: 0x000A25C7
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x000A43D0 File Offset: 0x000A25D0
		public TProperty GetProperty(TOwner owner, TMetadataValue value)
		{
			if (this.property != null)
			{
				return this.property;
			}
			this.property = new TProperty();
			this.property.Owner = owner;
			this.property.Parse(value);
			this.isDirty = false;
			return this.property;
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x000A442B File Offset: 0x000A262B
		public bool IsSamePropertyReference(TProperty property)
		{
			return this.property == property;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000A4440 File Offset: 0x000A2640
		public void SetProperty(TOwner owner, TProperty property, bool markAsDirty)
		{
			if (this.property != null)
			{
				this.property.Owner = default(TOwner);
			}
			this.property = property;
			this.property.Owner = owner;
			if (markAsDirty)
			{
				this.isDirty = true;
			}
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000A4495 File Offset: 0x000A2695
		public void UpdateProperty(TMetadataValue value)
		{
			if (this.property != null)
			{
				this.property.Parse(value);
				this.isDirty = false;
			}
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x000A44BC File Offset: 0x000A26BC
		public void Reset()
		{
			this.property = default(TProperty);
			this.isDirty = false;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x000A44D1 File Offset: 0x000A26D1
		internal void MarkAsDirty()
		{
			this.isDirty = true;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x000A44DA File Offset: 0x000A26DA
		internal void ExtractMetadataValueIfNeeded(ref TMetadataValue value, bool resetDirtyIndication)
		{
			if (this.property != null && this.isDirty)
			{
				value = this.property.Convert();
				if (resetDirtyIndication)
				{
					this.isDirty = false;
				}
			}
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x000A4511 File Offset: 0x000A2711
		internal bool TryUpdatePropertyFromJson(JToken json, out TMetadataValue value)
		{
			if (this.property != null && this.property.TryParseJson(json, out value))
			{
				this.isDirty = false;
				return true;
			}
			value = default(TMetadataValue);
			return false;
		}

		// Token: 0x040004B2 RID: 1202
		private TProperty property;

		// Token: 0x040004B3 RID: 1203
		private bool isDirty;
	}
}
