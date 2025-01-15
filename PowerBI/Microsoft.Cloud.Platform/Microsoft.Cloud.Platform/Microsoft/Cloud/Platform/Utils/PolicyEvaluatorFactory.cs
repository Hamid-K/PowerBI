using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200026C RID: 620
	public class PolicyEvaluatorFactory
	{
		// Token: 0x06001068 RID: 4200 RVA: 0x0003870D File Offset: 0x0003690D
		public PolicyEvaluatorFactory()
		{
			this.m_cache = new KeyedFromToCache<PolicyEvaluatorFactory.EvaluatorSpec, object>();
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00038720 File Offset: 0x00036920
		public T CreateEvaluator<T>(string id, object context, IEnumerable<string> rules, IEnumerable<string> explicitUsings, IEnumerable<string> explicitReferences, PolicyEvaluationResultsInspector resultsInspector) where T : class
		{
			object obj = null;
			PolicyEvaluatorFactory.EvaluatorSpec evaluatorSpec = new PolicyEvaluatorFactory.EvaluatorSpec(typeof(T), context, rules, explicitUsings, explicitReferences, resultsInspector);
			if (!string.IsNullOrWhiteSpace(id) && this.m_cache.TryGet(id, evaluatorSpec, out obj))
			{
				return obj as T;
			}
			string name2 = typeof(T).Name;
			string fullName = typeof(T).FullName;
			TypeAttributes attributes = typeof(T).Attributes;
			Ensure.ArgSatisfiesCondition(fullName, (attributes & TypeAttributes.ClassSemanticsMask) > TypeAttributes.NotPublic, "PolicyEvaluatorFactory.CreateEvaluator<T>() requires that T (" + fullName + ") be an interface");
			Ensure.ArgSatisfiesCondition("T", (attributes & TypeAttributes.Public) > TypeAttributes.NotPublic, "PolicyEvaluatorFactory.CreateEvaluator<T>() requires that T be public");
			string text = name2 + "_PolicyEvaluator";
			foreach (MethodInfo methodInfo in typeof(T).GetMethods())
			{
				if (!this.IsSpecialMethod(methodInfo))
				{
					Ensure.ArgSatisfiesCondition(fullName + "." + methodInfo.Name, methodInfo.ReturnType == typeof(bool) || methodInfo.ReturnType == typeof(void), "PolicyEvaluatorFactory.CreateEvaluator<T>() requires that all methods on T return either a void or a bool");
				}
			}
			List<string> list = new List<string> { "System", "Microsoft.Cloud.Platform.Utils" };
			string text2 = string.Join(Environment.NewLine, from use in list.Concat(explicitUsings).Distinct<string>()
				select "using " + use + ";");
			IEnumerable<string> enumerable = from ra in typeof(T).Assembly.GetReferencedAssemblies()
				from pa in AppDomain.CurrentDomain.GetAssemblies()
				where ra.FullName.Equals(pa.FullName, StringComparison.Ordinal)
				select pa.Location;
			List<string> list2 = new List<string>
			{
				typeof(T).Assembly.Location,
				base.GetType().Assembly.Location
			};
			string[] array2 = enumerable.Concat(explicitReferences).Concat(list2).Distinct<string>()
				.ToArray<string>();
			string text3 = "\r\n// Dynamically generated code by Microsoft.Cloud.Platform.Utils.PolicyEvaluator.\r\n{usings}\r\n\r\npublic class {generatedClassName} : {T}\r\n{\r\n    private string m_id;\r\n    private object m_context;\r\n    private PolicyEvaluationResultsInspector m_resultsInspector;\r\n    private string m_code;\r\n\r\n    // Constructor\r\n    public {generatedClassName}(string id, object context, PolicyEvaluationResultsInspector resultsInspector, string code)\r\n    {\r\n        m_id = id;\r\n        m_context = context;\r\n        m_resultsInspector = resultsInspector;\r\n        m_code = code;\r\n    }\r\n\r\n    // For debugging purposes\r\n    public string Code\r\n    {\r\n        get { return m_code; }\r\n    }\r\n\r\n    // Implementation of T's methods\r\n{Methods}}\r\n".Replace("{generatedClassName}", text);
			text3 = text3.Replace("{usings}", text2);
			text3 = text3.Replace("{T}", fullName);
			List<string> list3 = new List<string>();
			MethodInfo[] array = typeof(T).GetMethods();
			for (int i = 0; i < array.Length; i++)
			{
				MethodInfo methodInfo2 = array[i];
				if (!this.IsSpecialMethod(methodInfo2))
				{
					string text4 = "    public ";
					bool flag = methodInfo2.ReturnType == typeof(bool);
					text4 += (flag ? "bool " : "void ");
					text4 += methodInfo2.Name;
					ParameterInfo[] parameters = methodInfo2.GetParameters();
					IEnumerable<string> enumerable2 = parameters.Select((ParameterInfo parameter) => parameter.ParameterType.Name);
					IEnumerable<string> parametersNames = parameters.Select((ParameterInfo parameter) => parameter.Name);
					text4 += "(";
					IEnumerable<string> enumerable3 = enumerable2.Zip(parametersNames, (string type, string name) => type + " " + name);
					text4 += string.Join(", ", enumerable3);
					text4 = text4 + ")" + Environment.NewLine;
					text4 = text4 + "    {" + Environment.NewLine;
					text4 = text4 + "        bool result = false;" + Environment.NewLine;
					IEnumerable<string> enumerable4 = from rule in rules
						where parametersNames.Any((string name) => rule.IndexOf(name, StringComparison.Ordinal) != -1)
						select "(" + rule + ")";
					if (enumerable4.Any<string>())
					{
						text4 = text4 + "        result = " + Environment.NewLine;
						text4 = string.Concat(new string[]
						{
							text4,
							"            ",
							string.Join(Environment.NewLine + "            && ", enumerable4),
							";",
							Environment.NewLine
						});
					}
					text4 = text4 + "        if (m_resultsInspector != null)" + Environment.NewLine;
					text4 = text4 + "        {" + Environment.NewLine;
					text4 = string.Concat(new string[]
					{
						text4,
						"            m_resultsInspector(m_id, \"",
						methodInfo2.Name,
						"\", m_context, result);",
						Environment.NewLine
					});
					text4 = text4 + "        }" + Environment.NewLine;
					if (flag)
					{
						text4 = text4 + "        return result;" + Environment.NewLine;
					}
					text4 = text4 + "    }" + Environment.NewLine;
					list3.Add(text4);
				}
			}
			text3 = text3.Replace("{Methods}", string.Join(Environment.NewLine, list3));
			ExtendedCSharpCodeProvider extendedCSharpCodeProvider = new ExtendedCSharpCodeProvider(text3, array2, "");
			Assembly assembly = extendedCSharpCodeProvider.BuildAssembly(ExtendedCSharpCodeProviderBuildOptions.None, null);
			if (!extendedCSharpCodeProvider.IsDebugging())
			{
				text3 = "// Source code unavailable when not debugging";
			}
			obj = Activator.CreateInstance(assembly.GetType(text), new object[] { id, context, resultsInspector, text3 });
			Ensure.IsNotNull<object>(obj, "created policyEvaluator instance");
			if (!string.IsNullOrWhiteSpace(id))
			{
				this.m_cache.Put(id, evaluatorSpec, obj);
			}
			return obj as T;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00038D1C File Offset: 0x00036F1C
		private bool IsSpecialMethod(MethodInfo method)
		{
			return method.Name == "get_Code" && method.GetParameters().Length == 0 && method.ReturnType == typeof(string);
		}

		// Token: 0x0400061C RID: 1564
		private KeyedFromToCache<PolicyEvaluatorFactory.EvaluatorSpec, object> m_cache;

		// Token: 0x020006D8 RID: 1752
		[CannotApplyEqualityOperator]
		private class EvaluatorSpec : IEquatable<PolicyEvaluatorFactory.EvaluatorSpec>
		{
			// Token: 0x06002E9E RID: 11934 RVA: 0x000A256B File Offset: 0x000A076B
			public EvaluatorSpec(Type type, object context, IEnumerable<string> rules, IEnumerable<string> explicitUsings, IEnumerable<string> explicitReferences, PolicyEvaluationResultsInspector resultsInspector)
			{
				this.m_type = type;
				this.m_context = context;
				this.m_rules = rules;
				this.m_explicitUsings = explicitUsings;
				this.m_explicitReferences = explicitReferences;
				this.m_resultsInspector = resultsInspector;
			}

			// Token: 0x06002E9F RID: 11935 RVA: 0x000A25A0 File Offset: 0x000A07A0
			public bool Equals(PolicyEvaluatorFactory.EvaluatorSpec other)
			{
				return this.m_type == other.m_type && this.m_context == other.m_context && this.m_rules.SequenceEqual(other.m_rules) && this.m_explicitUsings.SequenceEqual(other.m_explicitUsings) && this.m_explicitReferences.SequenceEqual(other.m_explicitReferences) && this.m_resultsInspector == other.m_resultsInspector;
			}

			// Token: 0x06002EA0 RID: 11936 RVA: 0x000A2615 File Offset: 0x000A0815
			public override bool Equals(object obj)
			{
				return this.Equals(obj as PolicyEvaluatorFactory.EvaluatorSpec);
			}

			// Token: 0x06002EA1 RID: 11937 RVA: 0x000A2623 File Offset: 0x000A0823
			public override int GetHashCode()
			{
				ExtendedDiagnostics.EnsureOperation(false, "Can't call GetHashCode on an EvaluatorSpec object");
				return 0;
			}

			// Token: 0x0400136E RID: 4974
			private Type m_type;

			// Token: 0x0400136F RID: 4975
			private object m_context;

			// Token: 0x04001370 RID: 4976
			private IEnumerable<string> m_rules;

			// Token: 0x04001371 RID: 4977
			private IEnumerable<string> m_explicitUsings;

			// Token: 0x04001372 RID: 4978
			private IEnumerable<string> m_explicitReferences;

			// Token: 0x04001373 RID: 4979
			private PolicyEvaluationResultsInspector m_resultsInspector;
		}
	}
}
