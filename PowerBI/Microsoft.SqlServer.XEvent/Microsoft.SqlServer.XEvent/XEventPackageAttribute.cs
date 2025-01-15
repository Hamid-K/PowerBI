using System;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class XEventPackageAttribute : Attribute
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00002D88 File Offset: 0x00002D88
		public XEventPackageAttribute(string name, string packageGuid, string descriptionKey, string resourceBaseName)
		{
			this.Init(name, packageGuid, descriptionKey, resourceBaseName);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00002D64 File Offset: 0x00002D64
		public XEventPackageAttribute(string name, string packageGuid, string description)
		{
			this.Init(name, packageGuid, description, null);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00002BD4 File Offset: 0x00002BD4
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00002BF0 File Offset: 0x00002BF0
		public ValueType PackageGuid
		{
			get
			{
				return this.m_guid;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00002C0C File Offset: 0x00002C0C
		public string DescriptionKey
		{
			get
			{
				return this.m_descriptionKey;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00002C28 File Offset: 0x00002C28
		public string ResourceBaseName
		{
			get
			{
				return this.m_resourceBaseName;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00002C44 File Offset: 0x00002C44
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00002C60 File Offset: 0x00002C60
		internal bool IsRegistered
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_registered;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_registered = value;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00002B7C File Offset: 0x00002B7C
		private void Init(string name, string packageGuid, string description, string resourceBaseName)
		{
			XEventPackageRegistrar.CheckNamingRules(name);
			this.m_name = name;
			this.m_descriptionKey = description;
			ValueType valueType = default(Guid);
			(Guid)valueType = new Guid(packageGuid);
			this.m_guid = valueType;
			this.m_registered = false;
			this.m_resourceBaseName = resourceBaseName;
		}

		// Token: 0x040000E4 RID: 228
		private string m_name;

		// Token: 0x040000E5 RID: 229
		private string m_descriptionKey;

		// Token: 0x040000E6 RID: 230
		private string m_resourceBaseName;

		// Token: 0x040000E7 RID: 231
		private ValueType m_guid;

		// Token: 0x040000E8 RID: 232
		private bool m_registered;
	}
}
