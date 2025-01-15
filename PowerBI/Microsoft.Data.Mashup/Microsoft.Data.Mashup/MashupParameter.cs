using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Mashup.ProviderCommon;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000033 RID: 51
	public sealed class MashupParameter : DbParameter
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000AF10 File Offset: 0x00009110
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000AF18 File Offset: 0x00009118
		public override DbType DbType
		{
			get
			{
				return this.dbType;
			}
			set
			{
				this.dbType = value;
				this.typeWasSet = true;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000AF28 File Offset: 0x00009128
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000AF2F File Offset: 0x0000912F
		public override ParameterDirection Direction
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000AF36 File Offset: 0x00009136
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000AF3D File Offset: 0x0000913D
		public override bool IsNullable
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000AF44 File Offset: 0x00009144
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000AF4C File Offset: 0x0000914C
		public override string ParameterName
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000AF55 File Offset: 0x00009155
		public override void ResetDbType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000AF5C File Offset: 0x0000915C
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000AF63 File Offset: 0x00009163
		public override int Size
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000AF6A File Offset: 0x0000916A
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000AF71 File Offset: 0x00009171
		public override string SourceColumn
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000AF78 File Offset: 0x00009178
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000AF7F File Offset: 0x0000917F
		public override bool SourceColumnNullMapping
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000AF86 File Offset: 0x00009186
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000AF8D File Offset: 0x0000918D
		public override DataRowVersion SourceVersion
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000AF94 File Offset: 0x00009194
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000AF9C File Offset: 0x0000919C
		public override object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000AFA5 File Offset: 0x000091A5
		internal IParameter ToIParameter()
		{
			return new MashupParameter.Parameter(this);
		}

		// Token: 0x04000161 RID: 353
		private string name;

		// Token: 0x04000162 RID: 354
		private object value;

		// Token: 0x04000163 RID: 355
		private DbType dbType;

		// Token: 0x04000164 RID: 356
		private bool typeWasSet;

		// Token: 0x0200007C RID: 124
		private sealed class Parameter : IParameter
		{
			// Token: 0x060004D6 RID: 1238 RVA: 0x00011E8F File Offset: 0x0001008F
			public Parameter(MashupParameter mashupParameter)
			{
				this.mashupParameter = mashupParameter;
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00011E9E File Offset: 0x0001009E
			string IParameter.ParameterName
			{
				get
				{
					return this.mashupParameter.ParameterName;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00011EAB File Offset: 0x000100AB
			bool IParameter.TypeWasSet
			{
				get
				{
					return this.mashupParameter.typeWasSet;
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00011EB8 File Offset: 0x000100B8
			DbType IParameter.DbType
			{
				get
				{
					return this.mashupParameter.DbType;
				}
			}

			// Token: 0x04000292 RID: 658
			private readonly MashupParameter mashupParameter;
		}
	}
}
