using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000031 RID: 49
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class ColumnID
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00005485 File Offset: 0x00003685
		public ColumnID(string name)
			: this(Guid.Empty, name)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005493 File Offset: 0x00003693
		public ColumnID(Guid guid)
			: this(guid, string.Empty)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000054A1 File Offset: 0x000036A1
		public ColumnID(Guid guid, string name)
			: this(guid, name, (DBPROPID)0U)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000054AC File Offset: 0x000036AC
		public ColumnID(DBPROPID propertyID)
			: this(Guid.Empty, string.Empty, propertyID)
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000054BF File Offset: 0x000036BF
		public ColumnID(Guid guid, DBPROPID propertyID)
			: this(guid, string.Empty, propertyID)
		{
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000054CE File Offset: 0x000036CE
		private ColumnID(Guid guid, string name, DBPROPID propertyID)
		{
			this.name = name;
			this.guid = guid;
			this.propertyID = propertyID;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000054EB File Offset: 0x000036EB
		public bool HasGuid
		{
			get
			{
				return this.guid != Guid.Empty;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000054FD File Offset: 0x000036FD
		public bool HasName
		{
			get
			{
				return this.name.Length != 0;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000550D File Offset: 0x0000370D
		public bool HasPropertyID
		{
			get
			{
				return this.propertyID > (DBPROPID)0U;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00005518 File Offset: 0x00003718
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00005520 File Offset: 0x00003720
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005528 File Offset: 0x00003728
		public DBPROPID PropertyID
		{
			get
			{
				return this.propertyID;
			}
		}

		// Token: 0x0400004D RID: 77
		private readonly string name;

		// Token: 0x0400004E RID: 78
		private readonly Guid guid;

		// Token: 0x0400004F RID: 79
		private readonly DBPROPID propertyID;
	}
}
