using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200071F RID: 1823
	[Serializable]
	internal sealed class ActionInstance
	{
		// Token: 0x060065BA RID: 26042 RVA: 0x00190178 File Offset: 0x0018E378
		internal ActionInstance(ReportProcessing.ProcessingContext pc)
		{
			this.m_uniqueName = pc.CreateUniqueName();
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x0019018C File Offset: 0x0018E38C
		internal ActionInstance(ActionItemInstance actionItemInstance)
		{
			this.m_actionItemsValues = new ActionItemInstanceList();
			this.m_actionItemsValues.Add(actionItemInstance);
		}

		// Token: 0x060065BC RID: 26044 RVA: 0x001901AC File Offset: 0x0018E3AC
		internal ActionInstance()
		{
		}

		// Token: 0x17002403 RID: 9219
		// (get) Token: 0x060065BD RID: 26045 RVA: 0x001901B4 File Offset: 0x0018E3B4
		// (set) Token: 0x060065BE RID: 26046 RVA: 0x001901BC File Offset: 0x0018E3BC
		internal ActionItemInstanceList ActionItemsValues
		{
			get
			{
				return this.m_actionItemsValues;
			}
			set
			{
				this.m_actionItemsValues = value;
			}
		}

		// Token: 0x17002404 RID: 9220
		// (get) Token: 0x060065BF RID: 26047 RVA: 0x001901C5 File Offset: 0x0018E3C5
		// (set) Token: 0x060065C0 RID: 26048 RVA: 0x001901CD File Offset: 0x0018E3CD
		internal object[] StyleAttributeValues
		{
			get
			{
				return this.m_styleAttributeValues;
			}
			set
			{
				this.m_styleAttributeValues = value;
			}
		}

		// Token: 0x17002405 RID: 9221
		// (get) Token: 0x060065C1 RID: 26049 RVA: 0x001901D6 File Offset: 0x0018E3D6
		// (set) Token: 0x060065C2 RID: 26050 RVA: 0x001901DE File Offset: 0x0018E3DE
		internal int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x001901E7 File Offset: 0x0018E3E7
		internal object GetStyleAttributeValue(int index)
		{
			Global.Tracer.Assert(this.m_styleAttributeValues != null && 0 <= index && index < this.m_styleAttributeValues.Length);
			return this.m_styleAttributeValues[index];
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x00190218 File Offset: 0x0018E418
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.ActionItemList, ObjectType.ActionItemInstanceList),
				new MemberInfo(MemberName.StyleAttributeValues, Token.Array, ObjectType.Variant),
				new MemberInfo(MemberName.UniqueName, Token.Int32)
			});
		}

		// Token: 0x040032CF RID: 13007
		private ActionItemInstanceList m_actionItemsValues;

		// Token: 0x040032D0 RID: 13008
		private object[] m_styleAttributeValues;

		// Token: 0x040032D1 RID: 13009
		private int m_uniqueName;
	}
}
