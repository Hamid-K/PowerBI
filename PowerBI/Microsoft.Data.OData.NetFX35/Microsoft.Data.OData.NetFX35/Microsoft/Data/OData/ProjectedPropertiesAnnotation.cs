using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x02000242 RID: 578
	public sealed class ProjectedPropertiesAnnotation
	{
		// Token: 0x0600118A RID: 4490 RVA: 0x0004280F File Offset: 0x00040A0F
		public ProjectedPropertiesAnnotation(IEnumerable<string> projectedPropertyNames)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<string>>(projectedPropertyNames, "projectedPropertyNames");
			this.projectedProperties = new HashSet<string>(projectedPropertyNames, StringComparer.Ordinal);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00042833 File Offset: 0x00040A33
		internal ProjectedPropertiesAnnotation()
		{
			this.projectedProperties = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x0004284B File Offset: 0x00040A4B
		internal static ProjectedPropertiesAnnotation EmptyProjectedPropertiesInstance
		{
			get
			{
				return ProjectedPropertiesAnnotation.emptyProjectedPropertiesMarker;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00042852 File Offset: 0x00040A52
		internal static ProjectedPropertiesAnnotation AllProjectedPropertiesInstance
		{
			get
			{
				return ProjectedPropertiesAnnotation.allProjectedPropertiesMarker;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00042859 File Offset: 0x00040A59
		internal IEnumerable<string> ProjectedProperties
		{
			get
			{
				return this.projectedProperties;
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00042861 File Offset: 0x00040A61
		internal bool IsPropertyProjected(string propertyName)
		{
			return this.projectedProperties.Contains(propertyName);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0004286F File Offset: 0x00040A6F
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

		// Token: 0x06001191 RID: 4497 RVA: 0x0004289A File Offset: 0x00040A9A
		internal void Remove(string propertyName)
		{
			this.projectedProperties.Remove(propertyName);
		}

		// Token: 0x040006AD RID: 1709
		internal const string StarSegment = "*";

		// Token: 0x040006AE RID: 1710
		private static readonly ProjectedPropertiesAnnotation emptyProjectedPropertiesMarker = new ProjectedPropertiesAnnotation(new string[0]);

		// Token: 0x040006AF RID: 1711
		private static readonly ProjectedPropertiesAnnotation allProjectedPropertiesMarker = new ProjectedPropertiesAnnotation(new string[] { "*" });

		// Token: 0x040006B0 RID: 1712
		private readonly HashSet<string> projectedProperties;
	}
}
