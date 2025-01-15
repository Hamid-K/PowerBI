using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.StaticSqlUtil;

namespace Microsoft.HostIntegration.DrdaClient.Packages
{
	// Token: 0x02000A5D RID: 2653
	public class Section
	{
		// Token: 0x060052B9 RID: 21177 RVA: 0x0015048E File Offset: 0x0014E68E
		internal Section(Section section)
		{
			this.Parameters = new List<Parameter>();
			this._section = section;
			this.Sync(true);
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x001504AF File Offset: 0x0014E6AF
		public Section()
		{
			this.Parameters = new List<Parameter>();
			this._section = new Section();
			this.Sync(true);
		}

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x060052BB RID: 21179 RVA: 0x001504D4 File Offset: 0x0014E6D4
		// (set) Token: 0x060052BC RID: 21180 RVA: 0x001504E2 File Offset: 0x0014E6E2
		public ushort Number
		{
			get
			{
				return (ushort)this._section.PackageSectionNumber;
			}
			set
			{
				this._section.PackageSectionNumber = (int)value;
			}
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x060052BD RID: 21181 RVA: 0x001504F0 File Offset: 0x0014E6F0
		// (set) Token: 0x060052BE RID: 21182 RVA: 0x001504FD File Offset: 0x0014E6FD
		public string Alias
		{
			get
			{
				return this._section.PackageSectionAlias;
			}
			set
			{
				this._section.PackageSectionAlias = value;
			}
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x060052BF RID: 21183 RVA: 0x0015050B File Offset: 0x0014E70B
		// (set) Token: 0x060052C0 RID: 21184 RVA: 0x00150513 File Offset: 0x0014E713
		public List<Parameter> Parameters { get; private set; }

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x060052C1 RID: 21185 RVA: 0x0015051C File Offset: 0x0014E71C
		// (set) Token: 0x060052C2 RID: 21186 RVA: 0x00150524 File Offset: 0x0014E724
		public Statement Statement { get; private set; }

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x060052C3 RID: 21187 RVA: 0x0015052D File Offset: 0x0014E72D
		// (set) Token: 0x060052C4 RID: 21188 RVA: 0x00150535 File Offset: 0x0014E735
		public ResultSet ResultSet { get; private set; }

		// Token: 0x060052C5 RID: 21189 RVA: 0x0015053E File Offset: 0x0014E73E
		internal Section ToSection()
		{
			this.Sync(false);
			return this._section;
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x00150550 File Offset: 0x0014E750
		private void Sync(bool isRead)
		{
			if (isRead)
			{
				this.Parameters.Clear();
				foreach (Parameter parameter in this._section.Parameters)
				{
					this.Parameters.Add(new Parameter(parameter));
				}
				this.Statement = new Statement(this._section.Statement);
				this.ResultSet = new ResultSet(this._section.ResultSet);
				return;
			}
			this._section.Parameters.Clear();
			foreach (Parameter parameter2 in this.Parameters)
			{
				this._section.Parameters.Add(parameter2.ToParameter());
			}
			this._section.Statement = this.Statement.ToStatement();
			this._section.ResultSet = this.ResultSet.ToResultSet();
		}

		// Token: 0x04004151 RID: 16721
		private Section _section;
	}
}
