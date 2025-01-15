using System;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x02000791 RID: 1937
	internal class ODataBindingPath : IEquatable<ODataBindingPath>
	{
		// Token: 0x060038CD RID: 14541 RVA: 0x000B7114 File Offset: 0x000B5314
		public ODataBindingPath(Microsoft.OData.Edm.IEdmNavigationSource navigationSource, IEdmPathExpression bindingPath, Microsoft.OData.Edm.IEdmType targetType)
		{
			this.navigationSource = navigationSource;
			this.bindingPath = bindingPath;
			this.targetType = targetType;
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x060038CE RID: 14542 RVA: 0x000B7131 File Offset: 0x000B5331
		public Microsoft.OData.Edm.IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x060038CF RID: 14543 RVA: 0x000B7139 File Offset: 0x000B5339
		public IEdmPathExpression BindingPath
		{
			get
			{
				return this.bindingPath;
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x000B7141 File Offset: 0x000B5341
		public Microsoft.OData.Edm.IEdmType TargetType
		{
			get
			{
				return this.targetType;
			}
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000B714C File Offset: 0x000B534C
		public Microsoft.OData.Edm.IEdmProperty FindProperty(string name)
		{
			Microsoft.OData.Edm.IEdmStructuredType edmStructuredType = this.TargetType as Microsoft.OData.Edm.IEdmStructuredType;
			if (edmStructuredType == null)
			{
				return null;
			}
			return edmStructuredType.FindProperty(name);
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000B7174 File Offset: 0x000B5374
		public Microsoft.OData.Edm.IEdmNavigationSource FindNavigationTarget(Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty)
		{
			IEdmPathExpression edmPathExpression = new EdmPathExpression(this.bindingPath.PathSegments.Concat(new string[] { navigationProperty.Name }));
			return this.NavigationSource.FindNavigationTarget(navigationProperty, edmPathExpression);
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x000B71B4 File Offset: 0x000B53B4
		public bool Equals(ODataBindingPath other)
		{
			return this.navigationSource.Equals(other.navigationSource) && this.bindingPath.Path == other.bindingPath.Path && this.targetType.Equals(other.targetType);
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000B7204 File Offset: 0x000B5404
		public override bool Equals(object obj)
		{
			ODataBindingPath odataBindingPath = obj as ODataBindingPath;
			return odataBindingPath != null && this.Equals(odataBindingPath);
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000B7224 File Offset: 0x000B5424
		public override int GetHashCode()
		{
			return this.navigationSource.GetHashCode() ^ this.bindingPath.Path.GetHashCode() ^ this.targetType.GetHashCode();
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000B724E File Offset: 0x000B544E
		public ODataBindingPath AppendProperty(Microsoft.OData.Edm.IEdmProperty property)
		{
			return new ODataBindingPath(this.navigationSource, new EdmPathExpression(this.bindingPath.PathSegments.Concat(new string[] { property.Name })), property.Type.Definition);
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000B728A File Offset: 0x000B548A
		public ODataBindingPath AppendType(Microsoft.OData.Edm.IEdmType type)
		{
			return new ODataBindingPath(this.navigationSource, new EdmPathExpression(this.bindingPath.PathSegments.Concat(new string[] { type.FullTypeName() })), type);
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000B72BC File Offset: 0x000B54BC
		public static ODataBindingPath RootOf(Microsoft.OData.Edm.IEdmNavigationSource navigationSource)
		{
			return new ODataBindingPath(navigationSource, new EdmPathExpression(Array.Empty<string>()), navigationSource.EntityType());
		}

		// Token: 0x04001D57 RID: 7511
		private readonly Microsoft.OData.Edm.IEdmNavigationSource navigationSource;

		// Token: 0x04001D58 RID: 7512
		private readonly IEdmPathExpression bindingPath;

		// Token: 0x04001D59 RID: 7513
		private readonly Microsoft.OData.Edm.IEdmType targetType;
	}
}
