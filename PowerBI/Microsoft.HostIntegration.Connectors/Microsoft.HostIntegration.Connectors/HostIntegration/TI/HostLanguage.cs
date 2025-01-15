using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000735 RID: 1845
	[DataContract]
	[Serializable]
	public class HostLanguage : IHostLanguage
	{
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x000C64E1 File Offset: 0x000C46E1
		// (set) Token: 0x060039BE RID: 14782 RVA: 0x000C64E9 File Offset: 0x000C46E9
		[DataMember]
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000C64F2 File Offset: 0x000C46F2
		// (set) Token: 0x060039C0 RID: 14784 RVA: 0x000C64FA File Offset: 0x000C46FA
		[DataMember]
		public string DevImport
		{
			get
			{
				return this.devImport;
			}
			set
			{
				this.devImport = value;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000C6503 File Offset: 0x000C4703
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x000C650B File Offset: 0x000C470B
		[DataMember]
		public string LanguageExtension
		{
			get
			{
				return this.languageExtension;
			}
			set
			{
				this.languageExtension = value;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x060039C3 RID: 14787 RVA: 0x000C6514 File Offset: 0x000C4714
		// (set) Token: 0x060039C4 RID: 14788 RVA: 0x000C651C File Offset: 0x000C471C
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x000C6525 File Offset: 0x000C4725
		// (set) Token: 0x060039C6 RID: 14790 RVA: 0x000C652D File Offset: 0x000C472D
		[DataMember]
		public string FileExt
		{
			get
			{
				return this.fileExt;
			}
			set
			{
				this.fileExt = value;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x060039C7 RID: 14791 RVA: 0x000C6536 File Offset: 0x000C4736
		// (set) Token: 0x060039C8 RID: 14792 RVA: 0x000C653E File Offset: 0x000C473E
		[DataMember]
		public string ImporterAssembly
		{
			get
			{
				return this.importerAssembly;
			}
			set
			{
				this.importerAssembly = value;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x060039C9 RID: 14793 RVA: 0x000C6547 File Offset: 0x000C4747
		// (set) Token: 0x060039CA RID: 14794 RVA: 0x000C654F File Offset: 0x000C474F
		[DataMember]
		public string ImporterClass
		{
			get
			{
				return this.importerClass;
			}
			set
			{
				this.importerClass = value;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x060039CB RID: 14795 RVA: 0x000C6558 File Offset: 0x000C4758
		// (set) Token: 0x060039CC RID: 14796 RVA: 0x000C6560 File Offset: 0x000C4760
		[DataMember]
		public string ExporterGuid
		{
			get
			{
				return this.exporterguid;
			}
			set
			{
				this.exporterguid = value;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x060039CD RID: 14797 RVA: 0x000C6569 File Offset: 0x000C4769
		// (set) Token: 0x060039CE RID: 14798 RVA: 0x000C6571 File Offset: 0x000C4771
		[DataMember]
		public string Name
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

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x000C657A File Offset: 0x000C477A
		// (set) Token: 0x060039D0 RID: 14800 RVA: 0x000C6582 File Offset: 0x000C4782
		[DataMember]
		public string GUID
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x060039D1 RID: 14801 RVA: 0x000C658B File Offset: 0x000C478B
		// (set) Token: 0x060039D2 RID: 14802 RVA: 0x000036A9 File Offset: 0x000018A9
		[DataMember]
		public bool IsCOBOL
		{
			get
			{
				return this.name.ToUpper().Contains("COBOL");
			}
			set
			{
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x060039D3 RID: 14803 RVA: 0x000C65A2 File Offset: 0x000C47A2
		// (set) Token: 0x060039D4 RID: 14804 RVA: 0x000036A9 File Offset: 0x000018A9
		[DataMember]
		public bool IsRPG
		{
			get
			{
				return this.name.ToUpper().Contains("RPG");
			}
			set
			{
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000C65B9 File Offset: 0x000C47B9
		// (set) Token: 0x060039D6 RID: 14806 RVA: 0x000036A9 File Offset: 0x000018A9
		[DataMember]
		public bool IsPLI
		{
			get
			{
				return this.name.ToUpper().Contains("PLI");
			}
			set
			{
			}
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000C6514 File Offset: 0x000C4714
		public override string ToString()
		{
			return this.displayName;
		}

		// Token: 0x04002313 RID: 8979
		private string description;

		// Token: 0x04002314 RID: 8980
		private string devImport;

		// Token: 0x04002315 RID: 8981
		private string languageExtension;

		// Token: 0x04002316 RID: 8982
		private string displayName;

		// Token: 0x04002317 RID: 8983
		private string fileExt;

		// Token: 0x04002318 RID: 8984
		private string importerAssembly;

		// Token: 0x04002319 RID: 8985
		private string importerClass;

		// Token: 0x0400231A RID: 8986
		private string name;

		// Token: 0x0400231B RID: 8987
		private string guid;

		// Token: 0x0400231C RID: 8988
		private string exporterguid;
	}
}
