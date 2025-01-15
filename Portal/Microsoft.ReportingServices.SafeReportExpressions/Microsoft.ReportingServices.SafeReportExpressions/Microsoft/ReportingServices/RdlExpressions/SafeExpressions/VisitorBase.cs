using System;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000031 RID: 49
	internal abstract class VisitorBase
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00004E98 File Offset: 0x00003098
		protected string EvaluateIdentifierName(IdentifierNameSyntax identifierNameSyntax)
		{
			return identifierNameSyntax.Identifier.ValueText;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004EB4 File Offset: 0x000030B4
		protected string EvaluateSimpleName(SimpleNameSyntax simpleNameSyntax)
		{
			return simpleNameSyntax.Identifier.ValueText;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004ED0 File Offset: 0x000030D0
		protected void CheckDiagnostics(SyntaxNode node)
		{
			if (node.ContainsDiagnostics)
			{
				foreach (Diagnostic diagnostic in node.GetDiagnostics())
				{
					if (diagnostic.Severity == 3)
					{
						throw new SyntaxErrorException(diagnostic.GetMessage(null) ?? "");
					}
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004F40 File Offset: 0x00003140
		protected PropertyInfo GetPropertyInfo(Type type, string propertyName)
		{
			return type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004F4B File Offset: 0x0000314B
		protected MethodInfo GetMethodInfo(Type type, string methodName)
		{
			return type.GetMethod(methodName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, Type.EmptyTypes, null);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004F60 File Offset: 0x00003160
		protected MethodInfo GetMethodInfoWithOneReferenceTypeParameter(Type type, string methodName)
		{
			return type.GetMethods().FirstOrDefault(delegate(MethodInfo m)
			{
				if (m.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase))
				{
					ParameterInfo[] parameters = m.GetParameters();
					if (parameters.Length == 1)
					{
						return !parameters[0].ParameterType.IsValueType;
					}
				}
				return false;
			});
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004F91 File Offset: 0x00003191
		protected bool IsObjectReference(Type type)
		{
			return type == typeof(object);
		}

		// Token: 0x04000051 RID: 81
		private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
	}
}
