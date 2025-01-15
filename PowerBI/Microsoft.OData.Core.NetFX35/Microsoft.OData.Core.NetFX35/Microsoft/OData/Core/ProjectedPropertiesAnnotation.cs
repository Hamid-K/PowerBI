using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core
{
	// Token: 0x020001AB RID: 427
	public sealed class ProjectedPropertiesAnnotation
	{
		// Token: 0x06000FE4 RID: 4068 RVA: 0x000369EB File Offset: 0x00034BEB
		public ProjectedPropertiesAnnotation(IEnumerable<string> projectedPropertyNames)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<string>>(projectedPropertyNames, "projectedPropertyNames");
			this.projectedProperties = new HashSet<string>(projectedPropertyNames, StringComparer.Ordinal);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00036A0F File Offset: 0x00034C0F
		internal ProjectedPropertiesAnnotation()
		{
			this.projectedProperties = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00036A27 File Offset: 0x00034C27
		internal static ProjectedPropertiesAnnotation EmptyProjectedPropertiesInstance
		{
			get
			{
				return ProjectedPropertiesAnnotation.emptyProjectedPropertiesMarker;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00036A2E File Offset: 0x00034C2E
		internal static ProjectedPropertiesAnnotation AllProjectedPropertiesInstance
		{
			get
			{
				return ProjectedPropertiesAnnotation.allProjectedPropertiesMarker;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00036A35 File Offset: 0x00034C35
		internal IEnumerable<string> ProjectedProperties
		{
			get
			{
				return this.projectedProperties;
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00036A3D File Offset: 0x00034C3D
		internal bool IsPropertyProjected(string propertyName)
		{
			return this.projectedProperties.Contains(propertyName);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00036A4B File Offset: 0x00034C4B
		internal void Add(string propertyName)
		{
			if (object.ReferenceEquals(ProjectedPropertiesAnnotation.AllProjectedPropertiesInstance, this))
			{
				return;
			}
			if (!this.projectedProperties.Contains(propertyName))
			{
				this.projectedProperties.Add(propertyName);
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00036A76 File Offset: 0x00034C76
		internal void Remove(string propertyName)
		{
			this.projectedProperties.Remove(propertyName);
		}

		// Token: 0x040006FD RID: 1789
		internal const string StarSegment = "*";

		// Token: 0x040006FE RID: 1790
		private static readonly ProjectedPropertiesAnnotation emptyProjectedPropertiesMarker = new ProjectedPropertiesAnnotation(new string[0]);

		// Token: 0x040006FF RID: 1791
		private static readonly ProjectedPropertiesAnnotation allProjectedPropertiesMarker = new ProjectedPropertiesAnnotation(new string[] { "*" });

		// Token: 0x04000700 RID: 1792
		private readonly HashSet<string> projectedProperties;
	}
}
