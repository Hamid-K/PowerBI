using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation.Internal;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x0200021B RID: 539
	public static class EdmValidator
	{
		// Token: 0x06000CE3 RID: 3299 RVA: 0x00024664 File Offset: 0x00022864
		public static bool Validate(this IEdmModel root, out IEnumerable<EdmError> errors)
		{
			return root.Validate(root.GetEdmVersion() ?? EdmConstants.EdmVersionLatest, out errors);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002467C File Offset: 0x0002287C
		public static bool Validate(this IEdmModel root, Version version, out IEnumerable<EdmError> errors)
		{
			return root.Validate(ValidationRuleSet.GetEdmModelRuleSet(version), out errors);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002468B File Offset: 0x0002288B
		public static bool Validate(this IEdmModel root, ValidationRuleSet ruleSet, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(root, "root");
			EdmUtil.CheckArgumentNull<ValidationRuleSet>(ruleSet, "ruleSet");
			errors = InterfaceValidator.ValidateModelStructureAndSemantics(root, ruleSet);
			return Enumerable.FirstOrDefault<EdmError>(errors) == null;
		}
	}
}
