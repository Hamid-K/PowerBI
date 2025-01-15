using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200010F RID: 271
	[Serializable]
	public class QueryPermissionException : ResourceSecurityException
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x00005DA7 File Offset: 0x00003FA7
		public QueryPermissionException(IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
			: base(null, resource, query, null, null)
		{
			this.type = type;
			this.parameterCount = parameterCount;
			this.parameterNames = parameterNames;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00005DCB File Offset: 0x00003FCB
		public QueryPermissionException(IResource origin, IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
			: base(origin, resource, query, null, null)
		{
			this.type = type;
			this.parameterCount = parameterCount;
			this.parameterNames = parameterNames;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00005DF0 File Offset: 0x00003FF0
		protected QueryPermissionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			info.AddValue("type", (int)this.type);
			info.AddValue("parameterCount", this.parameterCount);
			info.AddValue("parameterName", this.parameterNames != null);
			for (int i = 0; i < this.parameterCount; i++)
			{
				info.AddValue("parameterName" + i.ToString(CultureInfo.InvariantCulture), this.parameterNames[i]);
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00005E70 File Offset: 0x00004070
		public QueryPermissionChallengeType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00005E78 File Offset: 0x00004078
		public int ParameterCount
		{
			get
			{
				return this.parameterCount;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00005E80 File Offset: 0x00004080
		public string[] ParameterNames
		{
			get
			{
				return this.parameterNames;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00005E88 File Offset: 0x00004088
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.type = (QueryPermissionChallengeType)info.GetInt32("type");
			this.parameterCount = info.GetInt32("parameterCount");
			this.parameterNames = null;
			if (info.GetBoolean("parameterName"))
			{
				this.parameterNames = new string[this.parameterCount];
				for (int i = 0; i < this.parameterCount; i++)
				{
					this.parameterNames[i] = info.GetString("parameterName" + i.ToString(CultureInfo.InvariantCulture));
				}
			}
			base.GetObjectData(info, context);
		}

		// Token: 0x04000297 RID: 663
		private const string typeName = "type";

		// Token: 0x04000298 RID: 664
		private const string parameterCountKey = "parameterCount";

		// Token: 0x04000299 RID: 665
		private const string parameterNamesKey = "parameterName";

		// Token: 0x0400029A RID: 666
		private QueryPermissionChallengeType type;

		// Token: 0x0400029B RID: 667
		private int parameterCount;

		// Token: 0x0400029C RID: 668
		private string[] parameterNames;
	}
}
