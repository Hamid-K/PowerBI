using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000D4 RID: 212
	public static class EdmValidator
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x0000F8CA File Offset: 0x0000DACA
		public static bool Validate(this IEdmModel root, out IEnumerable<EdmError> errors)
		{
			return root.Validate(root.GetEdmVersion() ?? EdmConstants.EdmVersionLatest, out errors);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000F8E2 File Offset: 0x0000DAE2
		public static bool Validate(this IEdmModel root, Version version, out IEnumerable<EdmError> errors)
		{
			return root.Validate(ValidationRuleSet.GetEdmModelRuleSet(version), out errors);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000F8F1 File Offset: 0x0000DAF1
		public static bool Validate(this IEdmModel root, ValidationRuleSet ruleSet, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(root, "root");
			EdmUtil.CheckArgumentNull<ValidationRuleSet>(ruleSet, "ruleSet");
			errors = InterfaceValidator.ValidateModelStructureAndSemantics(root, ruleSet);
			return Enumerable.FirstOrDefault<EdmError>(errors) == null;
		}
	}
}
