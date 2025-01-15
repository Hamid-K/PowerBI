using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000B3 RID: 179
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
	public sealed class ExpandAttribute : Attribute
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x00015652 File Offset: 0x00013852
		public ExpandAttribute()
		{
			this._defaultExpandType = new SelectExpandType?(SelectExpandType.Allowed);
			this._defaultMaxDepth = new int?(2);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00015680 File Offset: 0x00013880
		public ExpandAttribute(params string[] properties)
		{
			foreach (string text in properties)
			{
				if (!this._expandConfigurations.ContainsKey(text))
				{
					this._expandConfigurations.Add(text, new ExpandConfiguration
					{
						ExpandType = SelectExpandType.Allowed,
						MaxDepth = 2
					});
				}
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x000156DF File Offset: 0x000138DF
		public Dictionary<string, ExpandConfiguration> ExpandConfigurations
		{
			get
			{
				return this._expandConfigurations;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x000156E7 File Offset: 0x000138E7
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x000156F0 File Offset: 0x000138F0
		public SelectExpandType ExpandType
		{
			get
			{
				return this._expandType;
			}
			set
			{
				this._expandType = value;
				foreach (string text in this._expandConfigurations.Keys)
				{
					this._expandConfigurations[text].ExpandType = this._expandType;
				}
				if (this._expandConfigurations.Count == 0)
				{
					this._defaultExpandType = new SelectExpandType?(this._expandType);
				}
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00015780 File Offset: 0x00013980
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00015788 File Offset: 0x00013988
		public int MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				this._maxDepth = value;
				foreach (string text in this._expandConfigurations.Keys)
				{
					this._expandConfigurations[text].MaxDepth = this._maxDepth;
				}
				if (this._expandConfigurations.Count == 0)
				{
					this._defaultMaxDepth = new int?(this._maxDepth);
				}
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00015818 File Offset: 0x00013A18
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00015820 File Offset: 0x00013A20
		internal SelectExpandType? DefaultExpandType
		{
			get
			{
				return this._defaultExpandType;
			}
			set
			{
				this._defaultExpandType = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00015829 File Offset: 0x00013A29
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x00015831 File Offset: 0x00013A31
		internal int? DefaultMaxDepth
		{
			get
			{
				return this._defaultMaxDepth;
			}
			set
			{
				this._defaultMaxDepth = value;
			}
		}

		// Token: 0x0400016D RID: 365
		private readonly Dictionary<string, ExpandConfiguration> _expandConfigurations = new Dictionary<string, ExpandConfiguration>();

		// Token: 0x0400016E RID: 366
		private SelectExpandType _expandType;

		// Token: 0x0400016F RID: 367
		private SelectExpandType? _defaultExpandType;

		// Token: 0x04000170 RID: 368
		private int? _defaultMaxDepth;

		// Token: 0x04000171 RID: 369
		private int _maxDepth;
	}
}
