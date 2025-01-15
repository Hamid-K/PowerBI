using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200001F RID: 31
	internal sealed class DerivedTypeValidator
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00003C26 File Offset: 0x00001E26
		public DerivedTypeValidator(IEdmType expectedType, IEnumerable<string> derivedTypeConstraints, string resourceKind, string resourceName)
		{
			this.derivedTypeConstraints = derivedTypeConstraints;
			this.expectedType = expectedType;
			this.resourceKind = resourceKind;
			this.resourceName = resourceName;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00003C4B File Offset: 0x00001E4B
		internal void ValidateResourceType(IEdmType resourceType)
		{
			if (resourceType == null)
			{
				return;
			}
			this.ValidateResourceType(resourceType.FullTypeName());
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00003C60 File Offset: 0x00001E60
		internal void ValidateResourceType(string resourceTypeName)
		{
			if (resourceTypeName == null)
			{
				return;
			}
			if (this.expectedType != null && this.expectedType.FullTypeName() == resourceTypeName)
			{
				return;
			}
			if (this.derivedTypeConstraints == null || this.derivedTypeConstraints.Any((string c) => c == resourceTypeName))
			{
				return;
			}
			throw new ODataException(Strings.ReaderValidationUtils_ValueTypeNotAllowedInDerivedTypeConstraint(resourceTypeName, this.resourceKind, this.resourceName));
		}

		// Token: 0x0400005E RID: 94
		private IEnumerable<string> derivedTypeConstraints;

		// Token: 0x0400005F RID: 95
		private IEdmType expectedType;

		// Token: 0x04000060 RID: 96
		private string resourceKind;

		// Token: 0x04000061 RID: 97
		private string resourceName;
	}
}
