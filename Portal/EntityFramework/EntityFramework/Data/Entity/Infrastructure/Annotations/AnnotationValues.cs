using System;

namespace System.Data.Entity.Infrastructure.Annotations
{
	// Token: 0x020002C2 RID: 706
	public sealed class AnnotationValues
	{
		// Token: 0x0600221D RID: 8733 RVA: 0x0005FF03 File Offset: 0x0005E103
		public AnnotationValues(object oldValue, object newValue)
		{
			this._oldValue = oldValue;
			this._newValue = newValue;
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x0005FF19 File Offset: 0x0005E119
		public object OldValue
		{
			get
			{
				return this._oldValue;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x0005FF21 File Offset: 0x0005E121
		public object NewValue
		{
			get
			{
				return this._newValue;
			}
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0005FF29 File Offset: 0x0005E129
		private bool Equals(AnnotationValues other)
		{
			return object.Equals(this._oldValue, other._oldValue) && object.Equals(this._newValue, other._newValue);
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0005FF51 File Offset: 0x0005E151
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (obj is AnnotationValues && this.Equals((AnnotationValues)obj)));
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0005FF74 File Offset: 0x0005E174
		public override int GetHashCode()
		{
			return (((this._oldValue != null) ? this._oldValue.GetHashCode() : 0) * 397) ^ ((this._newValue != null) ? this._newValue.GetHashCode() : 0);
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0005FFA9 File Offset: 0x0005E1A9
		public static bool operator ==(AnnotationValues left, AnnotationValues right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0005FFB2 File Offset: 0x0005E1B2
		public static bool operator !=(AnnotationValues left, AnnotationValues right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x04000BDE RID: 3038
		private readonly object _oldValue;

		// Token: 0x04000BDF RID: 3039
		private readonly object _newValue;
	}
}
