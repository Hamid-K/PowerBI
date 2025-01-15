using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200070C RID: 1804
	[Serializable]
	internal sealed class RecordSetInfo
	{
		// Token: 0x060064D7 RID: 25815 RVA: 0x0018E9EB File Offset: 0x0018CBEB
		internal RecordSetInfo(bool readerExtensionsSupported, CompareOptions compareOptions)
		{
			this.m_readerExtensionsSupported = readerExtensionsSupported;
			this.m_compareOptions = compareOptions;
		}

		// Token: 0x060064D8 RID: 25816 RVA: 0x0018EA01 File Offset: 0x0018CC01
		internal RecordSetInfo()
		{
		}

		// Token: 0x170023B6 RID: 9142
		// (get) Token: 0x060064D9 RID: 25817 RVA: 0x0018EA09 File Offset: 0x0018CC09
		// (set) Token: 0x060064DA RID: 25818 RVA: 0x0018EA11 File Offset: 0x0018CC11
		internal bool ReaderExtensionsSupported
		{
			get
			{
				return this.m_readerExtensionsSupported;
			}
			set
			{
				this.m_readerExtensionsSupported = value;
			}
		}

		// Token: 0x170023B7 RID: 9143
		// (get) Token: 0x060064DB RID: 25819 RVA: 0x0018EA1A File Offset: 0x0018CC1A
		// (set) Token: 0x060064DC RID: 25820 RVA: 0x0018EA22 File Offset: 0x0018CC22
		internal RecordSetPropertyNamesList FieldPropertyNames
		{
			get
			{
				return this.m_fieldPropertyNames;
			}
			set
			{
				this.m_fieldPropertyNames = value;
			}
		}

		// Token: 0x170023B8 RID: 9144
		// (get) Token: 0x060064DD RID: 25821 RVA: 0x0018EA2B File Offset: 0x0018CC2B
		// (set) Token: 0x060064DE RID: 25822 RVA: 0x0018EA33 File Offset: 0x0018CC33
		internal CompareOptions CompareOptions
		{
			get
			{
				return this.m_compareOptions;
			}
			set
			{
				this.m_compareOptions = value;
			}
		}

		// Token: 0x170023B9 RID: 9145
		// (get) Token: 0x060064DF RID: 25823 RVA: 0x0018EA3C File Offset: 0x0018CC3C
		// (set) Token: 0x060064E0 RID: 25824 RVA: 0x0018EA44 File Offset: 0x0018CC44
		internal bool ValidCompareOptions
		{
			get
			{
				return this.m_validCompareOptions;
			}
			set
			{
				this.m_validCompareOptions = value;
			}
		}

		// Token: 0x060064E1 RID: 25825 RVA: 0x0018EA50 File Offset: 0x0018CC50
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.ReaderExtensionsSupported, Token.Boolean),
				new MemberInfo(MemberName.FieldPropertyNames, ObjectType.RecordSetPropertyNamesList),
				new MemberInfo(MemberName.CompareOptions, Token.Enum)
			});
		}

		// Token: 0x0400328B RID: 12939
		private bool m_readerExtensionsSupported;

		// Token: 0x0400328C RID: 12940
		private RecordSetPropertyNamesList m_fieldPropertyNames;

		// Token: 0x0400328D RID: 12941
		private CompareOptions m_compareOptions;

		// Token: 0x0400328E RID: 12942
		[NonSerialized]
		private bool m_validCompareOptions;
	}
}
