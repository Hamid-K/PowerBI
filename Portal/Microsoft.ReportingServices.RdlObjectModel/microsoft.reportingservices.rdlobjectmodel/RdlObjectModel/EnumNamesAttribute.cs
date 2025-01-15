using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001D6 RID: 470
	internal sealed class EnumNamesAttribute : Attribute
	{
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x000250C3 File Offset: 0x000232C3
		public IList<string> Names
		{
			get
			{
				return this.m_names;
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x000250CC File Offset: 0x000232CC
		public EnumNamesAttribute(Type type, string field)
		{
			string[] array = (string[])type.InvokeMember(field, BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture);
			this.m_names = new ReadOnlyCollection<string>(array);
		}

		// Token: 0x0400055D RID: 1373
		private readonly IList<string> m_names;
	}
}
