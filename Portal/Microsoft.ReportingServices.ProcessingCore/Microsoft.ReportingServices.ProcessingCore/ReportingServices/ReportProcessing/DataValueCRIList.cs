using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200075C RID: 1884
	[Serializable]
	internal sealed class DataValueCRIList : DataValueList
	{
		// Token: 0x0600684C RID: 26700 RVA: 0x00195F28 File Offset: 0x00194128
		internal DataValueCRIList()
		{
		}

		// Token: 0x0600684D RID: 26701 RVA: 0x00195F3E File Offset: 0x0019413E
		internal DataValueCRIList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170024D9 RID: 9433
		// (get) Token: 0x0600684E RID: 26702 RVA: 0x00195F55 File Offset: 0x00194155
		// (set) Token: 0x0600684F RID: 26703 RVA: 0x00195F5D File Offset: 0x0019415D
		internal int RDLRowIndex
		{
			get
			{
				return this.m_rdlRowIndex;
			}
			set
			{
				this.m_rdlRowIndex = value;
			}
		}

		// Token: 0x170024DA RID: 9434
		// (get) Token: 0x06006850 RID: 26704 RVA: 0x00195F66 File Offset: 0x00194166
		// (set) Token: 0x06006851 RID: 26705 RVA: 0x00195F6E File Offset: 0x0019416E
		internal int RDLColumnIndex
		{
			get
			{
				return this.m_rdlColumnIndex;
			}
			set
			{
				this.m_rdlColumnIndex = value;
			}
		}

		// Token: 0x06006852 RID: 26706 RVA: 0x00195F78 File Offset: 0x00194178
		internal new DataValueCRIList DeepClone(InitializationContext context)
		{
			int count = this.Count;
			DataValueCRIList dataValueCRIList = new DataValueCRIList(count);
			dataValueCRIList.RDLColumnIndex = this.m_rdlColumnIndex;
			dataValueCRIList.RDLRowIndex = this.m_rdlRowIndex;
			for (int i = 0; i < count; i++)
			{
				dataValueCRIList.Add(base[i].DeepClone(context));
			}
			return dataValueCRIList;
		}

		// Token: 0x06006853 RID: 26707 RVA: 0x00195FCC File Offset: 0x001941CC
		internal void Initialize(string prefix, InitializationContext context)
		{
			base.Initialize(prefix, this.m_rdlRowIndex, this.m_rdlColumnIndex, false, context);
		}

		// Token: 0x06006854 RID: 26708 RVA: 0x00195FE4 File Offset: 0x001941E4
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.DataValue, new MemberInfoList
			{
				new MemberInfo(MemberName.RDLRowIndex, Token.Int32),
				new MemberInfo(MemberName.RDLColumnIndex, Token.Int32)
			});
		}

		// Token: 0x04003388 RID: 13192
		private int m_rdlRowIndex = -1;

		// Token: 0x04003389 RID: 13193
		private int m_rdlColumnIndex = -1;
	}
}
