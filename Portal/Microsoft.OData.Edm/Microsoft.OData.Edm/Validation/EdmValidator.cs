using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000143 RID: 323
	public static class EdmValidator
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x00014C66 File Offset: 0x00012E66
		public static bool Validate(this IEdmModel root, out IEnumerable<EdmError> errors)
		{
			return root.Validate(root.GetEdmVersion() ?? EdmConstants.EdmVersionDefault, out errors);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00014C7E File Offset: 0x00012E7E
		public static bool Validate(this IEdmModel root, Version version, out IEnumerable<EdmError> errors)
		{
			return root.Validate(ValidationRuleSet.GetEdmModelRuleSet(version), out errors);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00014C8D File Offset: 0x00012E8D
		public static bool Validate(this IEdmModel root, ValidationRuleSet ruleSet, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(root, "root");
			EdmUtil.CheckArgumentNull<ValidationRuleSet>(ruleSet, "ruleSet");
			errors = InterfaceValidator.ValidateModelStructureAndSemantics(root, ruleSet);
			return errors.FirstOrDefault<EdmError>() == null;
		}
	}
}
