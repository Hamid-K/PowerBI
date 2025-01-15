using System;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200044A RID: 1098
	internal sealed class SapHanaMeasure : ICubeMeasure, ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x06002518 RID: 9496 RVA: 0x0006A371 File Offset: 0x00068571
		public SapHanaMeasure(string name, string caption, SapHanaAggregationType aggregationType, bool isAggregatable, TypeValue typeValue)
		{
			this.caption = caption;
			this.name = name;
			this.aggregationType = aggregationType;
			this.isAggregatable = isAggregatable;
			this.typeValue = typeValue;
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x000023C4 File Offset: 0x000005C4
		public CubeObjectKind Kind
		{
			get
			{
				return CubeObjectKind.Measure;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x0006A39E File Offset: 0x0006859E
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x0006A3A6 File Offset: 0x000685A6
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x0006A3AE File Offset: 0x000685AE
		public TypeValue TypeValue
		{
			get
			{
				return this.typeValue;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x0006A3B6 File Offset: 0x000685B6
		public SapHanaAggregationType AggregationType
		{
			get
			{
				return this.aggregationType;
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x0006A3BE File Offset: 0x000685BE
		public bool IsAggregatable
		{
			get
			{
				return this.isAggregatable;
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x0006A3C8 File Offset: 0x000685C8
		public bool IsAdditive
		{
			get
			{
				if (this.isAggregatable)
				{
					SapHanaAggregationType sapHanaAggregationType = this.AggregationType;
					if (sapHanaAggregationType == SapHanaAggregationType.Sum || sapHanaAggregationType - SapHanaAggregationType.Min <= 1)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x0006A3FA File Offset: 0x000685FA
		// (set) Token: 0x06002520 RID: 9504 RVA: 0x0006A3F1 File Offset: 0x000685F1
		public SapHanaColumn Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x0006A402 File Offset: 0x00068602
		public bool Equals(SapHanaMeasure other)
		{
			return other != null && this.name == other.name;
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x0006A41A File Offset: 0x0006861A
		public bool Equals(ICubeObject other)
		{
			return this.Equals(other as SapHanaMeasure);
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x0006A41A File Offset: 0x0006861A
		public override bool Equals(object other)
		{
			return this.Equals(other as SapHanaMeasure);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x0006A428 File Offset: 0x00068628
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x04000F16 RID: 3862
		private readonly string name;

		// Token: 0x04000F17 RID: 3863
		private readonly string caption;

		// Token: 0x04000F18 RID: 3864
		private readonly SapHanaAggregationType aggregationType;

		// Token: 0x04000F19 RID: 3865
		private readonly bool isAggregatable;

		// Token: 0x04000F1A RID: 3866
		private readonly TypeValue typeValue;

		// Token: 0x04000F1B RID: 3867
		private SapHanaColumn column;
	}
}
