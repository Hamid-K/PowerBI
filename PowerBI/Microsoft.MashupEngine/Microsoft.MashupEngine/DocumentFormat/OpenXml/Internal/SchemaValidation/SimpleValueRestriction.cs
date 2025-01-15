using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200312F RID: 12591
	[Serializable]
	internal abstract class SimpleValueRestriction<T, ST> : SimpleTypeRestriction where T : struct, IComparable<T> where ST : OpenXmlSimpleValue<T>, new()
	{
		// Token: 0x17009959 RID: 39257
		// (get) Token: 0x0601B4F7 RID: 111863
		protected abstract T MinValue { get; }

		// Token: 0x1700995A RID: 39258
		// (get) Token: 0x0601B4F8 RID: 111864
		protected abstract T MaxValue { get; }

		// Token: 0x0601B4F9 RID: 111865 RVA: 0x0037615A File Offset: 0x0037435A
		public SimpleValueRestriction()
		{
			this.MinInclusive = this.MinValue;
			this.MaxInclusive = this.MaxValue;
		}

		// Token: 0x1700995B RID: 39259
		// (get) Token: 0x0601B4FA RID: 111866 RVA: 0x0037617A File Offset: 0x0037437A
		public override string ClrTypeName
		{
			get
			{
				return SimpleValueRestriction<T, ST>.clrTypeName;
			}
		}

		// Token: 0x1700995C RID: 39260
		// (get) Token: 0x0601B4FB RID: 111867 RVA: 0x00376181 File Offset: 0x00374381
		// (set) Token: 0x0601B4FC RID: 111868 RVA: 0x00376189 File Offset: 0x00374389
		public T MinInclusive { get; set; }

		// Token: 0x1700995D RID: 39261
		// (get) Token: 0x0601B4FD RID: 111869 RVA: 0x00376192 File Offset: 0x00374392
		// (set) Token: 0x0601B4FE RID: 111870 RVA: 0x0037619A File Offset: 0x0037439A
		public T MaxInclusive { get; set; }

		// Token: 0x1700995E RID: 39262
		// (get) Token: 0x0601B4FF RID: 111871 RVA: 0x003761A3 File Offset: 0x003743A3
		// (set) Token: 0x0601B500 RID: 111872 RVA: 0x003761AB File Offset: 0x003743AB
		public T MinExclusive { get; set; }

		// Token: 0x1700995F RID: 39263
		// (get) Token: 0x0601B501 RID: 111873 RVA: 0x003761B4 File Offset: 0x003743B4
		// (set) Token: 0x0601B502 RID: 111874 RVA: 0x003761BC File Offset: 0x003743BC
		public T MaxExclusive { get; set; }

		// Token: 0x0601B503 RID: 111875 RVA: 0x003761C8 File Offset: 0x003743C8
		public override string GetRestrictionValue(RestrictionField restrictionField)
		{
			if (restrictionField <= RestrictionField.MaxInclusive)
			{
				if (restrictionField == RestrictionField.MinInclusive)
				{
					T minInclusive = this.MinInclusive;
					return minInclusive.ToString();
				}
				if (restrictionField == RestrictionField.MaxInclusive)
				{
					T maxInclusive = this.MaxInclusive;
					return maxInclusive.ToString();
				}
			}
			else
			{
				if (restrictionField == RestrictionField.MinExclusive)
				{
					T minExclusive = this.MinExclusive;
					return minExclusive.ToString();
				}
				if (restrictionField == RestrictionField.MaxExclusive)
				{
					T maxExclusive = this.MaxExclusive;
					return maxExclusive.ToString();
				}
			}
			return base.GetRestrictionValue(restrictionField);
		}

		// Token: 0x0601B504 RID: 111876 RVA: 0x00376250 File Offset: 0x00374450
		public virtual OpenXmlSimpleValue<T> StringToSimpleValue(string valueText)
		{
			if (string.IsNullOrEmpty(valueText))
			{
				return null;
			}
			ST st = new ST();
			st.InnerText = valueText;
			return st;
		}

		// Token: 0x0601B505 RID: 111877 RVA: 0x00376284 File Offset: 0x00374484
		public override bool IsMinInclusiveValid(OpenXmlSimpleType attributeValue)
		{
			ST st = (ST)((object)attributeValue);
			if ((byte)(base.RestrictionField & RestrictionField.MinInclusive) == 8)
			{
				T minInclusive = this.MinInclusive;
				if (minInclusive.CompareTo(st.Value) > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0601B506 RID: 111878 RVA: 0x003762CC File Offset: 0x003744CC
		public override bool IsMinExclusiveValid(OpenXmlSimpleType attributeValue)
		{
			ST st = (ST)((object)attributeValue);
			if ((byte)(base.RestrictionField & RestrictionField.MinExclusive) == 32)
			{
				T minExclusive = this.MinExclusive;
				if (minExclusive.CompareTo(st.Value) >= 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0601B507 RID: 111879 RVA: 0x00376318 File Offset: 0x00374518
		public override bool IsMaxInclusiveValid(OpenXmlSimpleType attributeValue)
		{
			ST st = (ST)((object)attributeValue);
			if ((byte)(base.RestrictionField & RestrictionField.MaxInclusive) == 16)
			{
				T maxInclusive = this.MaxInclusive;
				if (maxInclusive.CompareTo(st.Value) < 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0601B508 RID: 111880 RVA: 0x00376364 File Offset: 0x00374564
		public override bool IsMaxExclusiveValid(OpenXmlSimpleType attributeValue)
		{
			ST st = (ST)((object)attributeValue);
			if ((byte)(base.RestrictionField & RestrictionField.MaxExclusive) == 64)
			{
				T maxExclusive = this.MaxExclusive;
				if (maxExclusive.CompareTo(st.Value) <= 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0601B509 RID: 111881 RVA: 0x003763B0 File Offset: 0x003745B0
		public override bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			if (attributeValue.HasValue)
			{
				ST st = (ST)((object)attributeValue);
				T value = st.Value;
				if (value.CompareTo(this.MinValue) >= 0)
				{
					T value2 = st.Value;
					if (value2.CompareTo(this.MaxValue) <= 0)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0400B52A RID: 46378
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = typeof(T).Name;
	}
}
