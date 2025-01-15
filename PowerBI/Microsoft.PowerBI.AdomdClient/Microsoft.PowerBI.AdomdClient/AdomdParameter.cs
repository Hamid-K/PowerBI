using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000061 RID: 97
	public sealed class AdomdParameter : MarshalByRefObject, IDbDataParameter, IDataParameter, ICloneable
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x00022155 File Offset: 0x00020355
		public AdomdParameter()
		{
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0002215D File Offset: 0x0002035D
		public AdomdParameter(string parameterName, object value)
		{
			this.CheckParameterValueType(value, "value");
			this.parameterName = parameterName;
			this.parameterValue = value;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0002217F File Offset: 0x0002037F
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00022187 File Offset: 0x00020387
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0002218E File Offset: 0x0002038E
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

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00022195 File Offset: 0x00020395
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00022198 File Offset: 0x00020398
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

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x000221A4 File Offset: 0x000203A4
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x000221A7 File Offset: 0x000203A7
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

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x000221AE File Offset: 0x000203AE
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x000221C4 File Offset: 0x000203C4
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

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x000221DB File Offset: 0x000203DB
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x000221E3 File Offset: 0x000203E3
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

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x000221EC File Offset: 0x000203EC
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x000221F3 File Offset: 0x000203F3
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

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x000221FA File Offset: 0x000203FA
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x00022201 File Offset: 0x00020401
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

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00022213 File Offset: 0x00020413
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0002221B File Offset: 0x0002041B
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

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00022230 File Offset: 0x00020430
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x00022237 File Offset: 0x00020437
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

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0002223E File Offset: 0x0002043E
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x00022245 File Offset: 0x00020445
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

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0002224C File Offset: 0x0002044C
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x00022253 File Offset: 0x00020453
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

		// Token: 0x06000658 RID: 1624 RVA: 0x0002225A File Offset: 0x0002045A
		public AdomdParameter Clone()
		{
			return new AdomdParameter(this.parameterName, this.parameterValue);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0002226D File Offset: 0x0002046D
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00022278 File Offset: 0x00020478
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

		// Token: 0x04000445 RID: 1093
		private AdomdParameterCollection parent;

		// Token: 0x04000446 RID: 1094
		private string parameterName;

		// Token: 0x04000447 RID: 1095
		private object parameterValue;
	}
}
