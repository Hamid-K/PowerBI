using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200076A RID: 1898
	[Serializable]
	internal sealed class DataValueInstance
	{
		// Token: 0x17002533 RID: 9523
		// (get) Token: 0x06006950 RID: 26960 RVA: 0x00199CEC File Offset: 0x00197EEC
		// (set) Token: 0x06006951 RID: 26961 RVA: 0x00199CF4 File Offset: 0x00197EF4
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17002534 RID: 9524
		// (get) Token: 0x06006952 RID: 26962 RVA: 0x00199CFD File Offset: 0x00197EFD
		// (set) Token: 0x06006953 RID: 26963 RVA: 0x00199D05 File Offset: 0x00197F05
		internal object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x06006954 RID: 26964 RVA: 0x00199D10 File Offset: 0x00197F10
		internal DataValueInstance DeepClone()
		{
			DataValueInstance dataValueInstance = new DataValueInstance();
			if (this.m_name != null)
			{
				dataValueInstance.Name = string.Copy(this.m_name);
			}
			if (this.m_value != null)
			{
				object obj;
				CustomReportItem.CloneObject(this.m_value, out obj);
				dataValueInstance.Value = obj;
			}
			return dataValueInstance;
		}

		// Token: 0x06006955 RID: 26965 RVA: 0x00199D5C File Offset: 0x00197F5C
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Value, Token.Object)
			});
		}

		// Token: 0x040033DF RID: 13279
		private string m_name;

		// Token: 0x040033E0 RID: 13280
		private object m_value;
	}
}
