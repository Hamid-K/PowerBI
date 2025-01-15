using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002075 RID: 8309
	public class FormulaConfig
	{
		// Token: 0x0600CB57 RID: 52055 RVA: 0x002884BE File Offset: 0x002866BE
		public FormulaConfig()
		{
			this.resources = new List<Resource>();
			this.embeddings = new List<string>();
		}

		// Token: 0x0600CB58 RID: 52056 RVA: 0x002884DC File Offset: 0x002866DC
		public FormulaConfig(string sectionName, string formulaName)
			: this()
		{
			this.sectionName = sectionName;
			this.formulaName = formulaName;
		}

		// Token: 0x170030F5 RID: 12533
		// (get) Token: 0x0600CB59 RID: 52057 RVA: 0x002884F2 File Offset: 0x002866F2
		// (set) Token: 0x0600CB5A RID: 52058 RVA: 0x002884FA File Offset: 0x002866FA
		[XmlAttribute]
		public string SectionName
		{
			get
			{
				return this.sectionName;
			}
			set
			{
				this.sectionName = value;
			}
		}

		// Token: 0x170030F6 RID: 12534
		// (get) Token: 0x0600CB5B RID: 52059 RVA: 0x00288503 File Offset: 0x00286703
		// (set) Token: 0x0600CB5C RID: 52060 RVA: 0x0028850B File Offset: 0x0028670B
		[XmlAttribute]
		public string FormulaName
		{
			get
			{
				return this.formulaName;
			}
			set
			{
				this.formulaName = value;
			}
		}

		// Token: 0x170030F7 RID: 12535
		// (get) Token: 0x0600CB5D RID: 52061 RVA: 0x00288514 File Offset: 0x00286714
		// (set) Token: 0x0600CB5E RID: 52062 RVA: 0x0028851C File Offset: 0x0028671C
		[XmlAttribute]
		public bool Published
		{
			get
			{
				return this.published;
			}
			set
			{
				this.published = value;
			}
		}

		// Token: 0x170030F8 RID: 12536
		// (get) Token: 0x0600CB5F RID: 52063 RVA: 0x00288525 File Offset: 0x00286725
		[XmlArray("Resources")]
		[XmlArrayItem("Resource")]
		public List<Resource> Resources
		{
			get
			{
				return this.resources;
			}
		}

		// Token: 0x170030F9 RID: 12537
		// (get) Token: 0x0600CB60 RID: 52064 RVA: 0x0028852D File Offset: 0x0028672D
		[XmlArray("Embeddings")]
		[XmlArrayItem("Embedding")]
		public List<string> Embeddings
		{
			get
			{
				return this.embeddings;
			}
		}

		// Token: 0x0400673C RID: 26428
		private string sectionName;

		// Token: 0x0400673D RID: 26429
		private string formulaName;

		// Token: 0x0400673E RID: 26430
		private List<Resource> resources;

		// Token: 0x0400673F RID: 26431
		private List<string> embeddings;

		// Token: 0x04006740 RID: 26432
		private bool published;
	}
}
