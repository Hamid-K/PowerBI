using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000061 RID: 97
	public sealed class AdomdParameter : MarshalByRefObject, IDbDataParameter, IDataParameter, ICloneable
	{
		// Token: 0x0600064C RID: 1612 RVA: 0x00022485 File Offset: 0x00020685
		public AdomdParameter()
		{
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0002248D File Offset: 0x0002068D
		public AdomdParameter(string parameterName, object value)
		{
			this.CheckParameterValueType(value, "value");
			this.parameterName = parameterName;
			this.parameterValue = value;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000224AF File Offset: 0x000206AF
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x000224B7 File Offset: 0x000206B7
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x000224BE File Offset: 0x000206BE
		public DbType DbType
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

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x000224C5 File Offset: 0x000206C5
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x000224C8 File Offset: 0x000206C8
		public ParameterDirection Direction
		{
			get
			{
				return ParameterDirection.Input;
			}
			set
			{
				if (ParameterDirection.Input != value)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x000224D4 File Offset: 0x000206D4
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x000224D7 File Offset: 0x000206D7
		public bool IsNullable
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x000224DE File Offset: 0x000206DE
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x000224F4 File Offset: 0x000206F4
		public string ParameterName
		{
			get
			{
				if (this.parameterName == null)
				{
					return string.Empty;
				}
				return this.parameterName;
			}
			set
			{
				if (this.parameterName != value)
				{
					this.parameterName = value;
				}
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0002250B File Offset: 0x0002070B
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x00022513 File Offset: 0x00020713
		internal AdomdParameterCollection Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0002251C File Offset: 0x0002071C
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x00022523 File Offset: 0x00020723
		public string SourceColumn
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

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0002252A File Offset: 0x0002072A
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x00022531 File Offset: 0x00020731
		public DataRowVersion SourceVersion
		{
			get
			{
				return DataRowVersion.Current;
			}
			set
			{
				if (value != DataRowVersion.Current)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00022543 File Offset: 0x00020743
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x0002254B File Offset: 0x0002074B
		public object Value
		{
			get
			{
				return this.parameterValue;
			}
			set
			{
				this.CheckParameterValueType(value, "value");
				this.parameterValue = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00022560 File Offset: 0x00020760
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x00022567 File Offset: 0x00020767
		public byte Precision
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

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0002256E File Offset: 0x0002076E
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x00022575 File Offset: 0x00020775
		public byte Scale
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

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0002257C File Offset: 0x0002077C
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x00022583 File Offset: 0x00020783
		public int Size
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

		// Token: 0x06000665 RID: 1637 RVA: 0x0002258A File Offset: 0x0002078A
		public AdomdParameter Clone()
		{
			return new AdomdParameter(this.parameterName, this.parameterValue);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0002259D File Offset: 0x0002079D
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000225A8 File Offset: 0x000207A8
		private void CheckParameterValueType(object value, string argumentName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(argumentName);
			}
			Type type = value.GetType();
			if (!XmlaClient.IsTypeSupportedForParameters(type))
			{
				throw new ArgumentException(SR.ArgumentErrorUnsupportedParameterType(type.FullName), argumentName);
			}
		}

		// Token: 0x04000452 RID: 1106
		private AdomdParameterCollection parent;

		// Token: 0x04000453 RID: 1107
		private string parameterName;

		// Token: 0x04000454 RID: 1108
		private object parameterValue;
	}
}
