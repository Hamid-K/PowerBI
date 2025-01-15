using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002088 RID: 8328
	public abstract class QueryPermission : IEquatable<QueryPermission>
	{
		// Token: 0x0600CBC2 RID: 52162 RVA: 0x0028942D File Offset: 0x0028762D
		public QueryPermission()
		{
			this.resource = new Resource();
		}

		// Token: 0x0600CBC3 RID: 52163 RVA: 0x00289440 File Offset: 0x00287640
		public QueryPermission(Resource resource, QueryPermissionChallengeType type, string queryPermissionData)
		{
			this.resource = resource;
			this.type = type;
			this.queryPermissionData = queryPermissionData;
		}

		// Token: 0x1700310F RID: 12559
		// (get) Token: 0x0600CBC4 RID: 52164 RVA: 0x0028945D File Offset: 0x0028765D
		// (set) Token: 0x0600CBC5 RID: 52165 RVA: 0x00289465 File Offset: 0x00287665
		[XmlIgnore]
		public Resource Resource
		{
			get
			{
				return this.resource;
			}
			set
			{
				this.resource = value;
			}
		}

		// Token: 0x17003110 RID: 12560
		// (get) Token: 0x0600CBC6 RID: 52166 RVA: 0x0028946E File Offset: 0x0028766E
		// (set) Token: 0x0600CBC7 RID: 52167 RVA: 0x0028947B File Offset: 0x0028767B
		[XmlAttribute("ResourceKind")]
		public string ResourceKind
		{
			get
			{
				return this.resource.Kind;
			}
			set
			{
				this.resource.Kind = value;
			}
		}

		// Token: 0x17003111 RID: 12561
		// (get) Token: 0x0600CBC8 RID: 52168 RVA: 0x00289489 File Offset: 0x00287689
		// (set) Token: 0x0600CBC9 RID: 52169 RVA: 0x00289496 File Offset: 0x00287696
		[XmlAttribute("ResourcePath")]
		public string ResourcePath
		{
			get
			{
				return this.resource.Path;
			}
			set
			{
				this.resource.Path = value;
			}
		}

		// Token: 0x17003112 RID: 12562
		// (get) Token: 0x0600CBCA RID: 52170 RVA: 0x002894A4 File Offset: 0x002876A4
		// (set) Token: 0x0600CBCB RID: 52171 RVA: 0x002894B1 File Offset: 0x002876B1
		[XmlAttribute("NonNormalizedResourcePath")]
		public string NonNormalizedResourcePath
		{
			get
			{
				return this.resource.NonNormalizedPath;
			}
			set
			{
				this.resource.NonNormalizedPath = value;
			}
		}

		// Token: 0x17003113 RID: 12563
		// (get) Token: 0x0600CBCC RID: 52172 RVA: 0x002894BF File Offset: 0x002876BF
		// (set) Token: 0x0600CBCD RID: 52173 RVA: 0x002894C7 File Offset: 0x002876C7
		[XmlAttribute("QueryPermissionType")]
		public QueryPermissionChallengeType QueryPermissionType
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

		// Token: 0x17003114 RID: 12564
		// (get) Token: 0x0600CBCE RID: 52174
		// (set) Token: 0x0600CBCF RID: 52175
		[XmlIgnore]
		public abstract string QueryPermissionData { get; set; }

		// Token: 0x0600CBD0 RID: 52176 RVA: 0x002894D0 File Offset: 0x002876D0
		private static QueryPermissionChallengeType GetQueryPermissionChallengeType(string type)
		{
			return (QueryPermissionChallengeType)Enum.Parse(typeof(QueryPermissionChallengeType), type);
		}

		// Token: 0x0600CBD1 RID: 52177 RVA: 0x002894E8 File Offset: 0x002876E8
		public static bool Lookup(QueryPermission[] queryPermissions, Resource resource, QueryPermissionChallengeType type, string queryPermissionData, int parameterCount, IEnumerable<string> parameterNames)
		{
			for (int i = 0; i < queryPermissions.Length; i++)
			{
				if (queryPermissions[i].Matches(resource, type, queryPermissionData, parameterCount, parameterNames))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600CBD2 RID: 52178 RVA: 0x0028951C File Offset: 0x0028771C
		public static QueryPermission[] Lookup(QueryPermission[] queryPermissions, Resource resource)
		{
			IList<QueryPermission> list = new List<QueryPermission>();
			int num = queryPermissions.Length;
			for (int i = 0; i < num; i++)
			{
				QueryPermission queryPermission = queryPermissions[i];
				if (queryPermission.ResourceKind == resource.Kind && queryPermission.ResourcePath.StartsWith(resource.Path, StringComparison.Ordinal))
				{
					list.Add(queryPermission);
				}
			}
			return list.ToArray<QueryPermission>();
		}

		// Token: 0x0600CBD3 RID: 52179 RVA: 0x00002E09 File Offset: 0x00001009
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600CBD4 RID: 52180 RVA: 0x00289577 File Offset: 0x00287777
		public override bool Equals(object other)
		{
			return this.Equals(other as QueryPermission);
		}

		// Token: 0x0600CBD5 RID: 52181 RVA: 0x00289585 File Offset: 0x00287785
		public bool Equals(QueryPermission other)
		{
			return other != null && (this.ResourceKind == other.ResourceKind && this.ResourcePath == other.ResourcePath) && this.QueryPermissionData == other.QueryPermissionData;
		}

		// Token: 0x0600CBD6 RID: 52182 RVA: 0x002895C5 File Offset: 0x002877C5
		public virtual bool Matches(Resource resource, QueryPermissionChallengeType type, string queryPermissionData, int parameterCount, IEnumerable<string> parameterNames)
		{
			return parameterCount <= 0 && (this.Resource.Equals(resource) && string.CompareOrdinal(this.queryPermissionData, queryPermissionData) == 0) && this.QueryPermissionType == type;
		}

		// Token: 0x04006764 RID: 26468
		protected Resource resource;

		// Token: 0x04006765 RID: 26469
		protected string queryPermissionData;

		// Token: 0x04006766 RID: 26470
		private QueryPermissionChallengeType type;
	}
}
