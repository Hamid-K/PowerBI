using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000BB RID: 187
	[Guid("418EFEC0-6B8C-45ca-A1AE-91DB5FF682DC")]
	public sealed class ServerProperty : ICloneable
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x00028E37 File Offset: 0x00027037
		object ICloneable.Clone()
		{
			return this.CopyTo(new ServerProperty());
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00028E44 File Offset: 0x00027044
		public ServerProperty()
		{
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00028E98 File Offset: 0x00027098
		public ServerProperty(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00028EF7 File Offset: 0x000270F7
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00028F00 File Offset: 0x00027100
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.name != value)
				{
					if (this.owningCollection != null)
					{
						throw new InvalidOperationException(SR.PropertyCannotBeChangedForObjectInCollection("Name", typeof(ServerProperty).Name));
					}
					this.name = Utils.Trim(value);
				}
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00028F4E File Offset: 0x0002714E
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00028F56 File Offset: 0x00027156
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00028F5F File Offset: 0x0002715F
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00028F67 File Offset: 0x00027167
		public string Value
		{
			get
			{
				return this.currentValue;
			}
			set
			{
				this.currentValue = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00028F70 File Offset: 0x00027170
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00028F78 File Offset: 0x00027178
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00028F81 File Offset: 0x00027181
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x00028F89 File Offset: 0x00027189
		public string PendingValue
		{
			get
			{
				return this.pendingValue;
			}
			set
			{
				this.pendingValue = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00028F92 File Offset: 0x00027192
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x00028F9A File Offset: 0x0002719A
		public bool RequiresRestart
		{
			get
			{
				return this.requiresRestart;
			}
			set
			{
				this.requiresRestart = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00028FA3 File Offset: 0x000271A3
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x00028FAB File Offset: 0x000271AB
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00028FB4 File Offset: 0x000271B4
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x00028FBC File Offset: 0x000271BC
		public bool DisplayFlag
		{
			get
			{
				return this.displayFlag;
			}
			set
			{
				this.displayFlag = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00028FC5 File Offset: 0x000271C5
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x00028FCD File Offset: 0x000271CD
		public ServerPropertyCategory Category
		{
			get
			{
				return this.category;
			}
			set
			{
				this.category = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00028FD6 File Offset: 0x000271D6
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x00028FDE File Offset: 0x000271DE
		public string Units
		{
			get
			{
				return this.units;
			}
			set
			{
				this.units = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00028FE7 File Offset: 0x000271E7
		[XmlIgnore]
		public string PropertyName
		{
			get
			{
				if (this.name == null)
				{
					return string.Empty;
				}
				return this.name.Substring(1 + this.name.LastIndexOf('\\'));
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00029011 File Offset: 0x00027211
		[XmlIgnore]
		public string FolderName
		{
			get
			{
				if (this.name == null)
				{
					return string.Empty;
				}
				return this.name.Substring(0, 1 + this.name.LastIndexOf('\\'));
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0002903C File Offset: 0x0002723C
		public ServerProperty CopyTo(ServerProperty obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (obj == this)
			{
				throw new InvalidOperationException(SR.Copy_DestinationIsSelf);
			}
			obj.Name = this.Name;
			obj.Type = this.Type;
			obj.Value = this.Value;
			obj.DefaultValue = this.DefaultValue;
			obj.PendingValue = this.PendingValue;
			obj.RequiresRestart = this.RequiresRestart;
			obj.IsReadOnly = this.IsReadOnly;
			obj.DisplayFlag = this.DisplayFlag;
			obj.Category = this.Category;
			obj.Units = this.Units;
			return obj;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000290DF File Offset: 0x000272DF
		public ServerProperty Clone()
		{
			return this.CopyTo(new ServerProperty());
		}

		// Token: 0x04000501 RID: 1281
		internal ServerPropertyCollection owningCollection;

		// Token: 0x04000502 RID: 1282
		private string name = string.Empty;

		// Token: 0x04000503 RID: 1283
		private string type = string.Empty;

		// Token: 0x04000504 RID: 1284
		private string currentValue = string.Empty;

		// Token: 0x04000505 RID: 1285
		private string defaultValue = string.Empty;

		// Token: 0x04000506 RID: 1286
		private string pendingValue = string.Empty;

		// Token: 0x04000507 RID: 1287
		private bool requiresRestart;

		// Token: 0x04000508 RID: 1288
		private bool isReadOnly;

		// Token: 0x04000509 RID: 1289
		private bool displayFlag = true;

		// Token: 0x0400050A RID: 1290
		private ServerPropertyCategory category;

		// Token: 0x0400050B RID: 1291
		private string units;
	}
}
