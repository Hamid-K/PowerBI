using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004EB RID: 1259
	internal static class ProxyGenerator<TContract> where TContract : class
	{
		// Token: 0x06002637 RID: 9783 RVA: 0x000878A4 File Offset: 0x00085AA4
		private static string GetInterfaceClassName(Type iface)
		{
			string text = iface.Name;
			if (iface.Name[0].Equals('I'))
			{
				text = text.Substring(1, text.Length - 1);
			}
			return text;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000878E4 File Offset: 0x00085AE4
		internal static ProxyCreator Generate(ProxyGenerationOptions proxyGenerationOptions)
		{
			string text = Uri.UnescapeDataString(new UriBuilder(Assembly.GetAssembly(typeof(TContract)).CodeBase).Path);
			string text2 = Path.GetFileNameWithoutExtension(text) + ".EcfProxys";
			string text3 = Path.GetDirectoryName(text) + "\\" + text2 + ".dll";
			if (File.Exists(text3))
			{
				Assembly assembly = Assembly.Load(AssemblyName.GetAssemblyName(text3));
				string text4 = typeof(TContract).Namespace + ".ECFProxies." + proxyGenerationOptions.ToString();
				TraceSourceBase<UtilsTrace>.Tracer.TraceInformation("Found Proxy in already loaded dll: Loading class {0} from {1} from file {2}".FormatWithInvariantCulture(new object[]
				{
					string.Concat(new string[]
					{
						text4,
						".",
						ProxyGenerator<TContract>.GetInterfaceClassName(typeof(TContract)),
						"Proxy",
						proxyGenerationOptions.ToString()
					}),
					text4,
					text3
				}));
				return new ProxyCreator(assembly, text4, string.Concat(new string[]
				{
					text4,
					".",
					ProxyGenerator<TContract>.GetInterfaceClassName(typeof(TContract)),
					"Proxy",
					proxyGenerationOptions.ToString()
				}), true);
			}
			Type typeFromHandle = typeof(TContract);
			ContractObjectModel contractObjectModel = new ContractObjectModel(typeFromHandle, proxyGenerationOptions);
			List<string> list = (from ra in typeFromHandle.Assembly.GetReferencedAssemblies()
				from pa in AppDomain.CurrentDomain.GetAssemblies()
				where ra.FullName.Equals(pa.FullName, StringComparison.Ordinal)
				select pa.Location).Distinct(new Func<string, string>(Path.GetFileName)).ToList<string>();
			list.Add(typeFromHandle.Assembly.Location);
			return new ProxyCreator(new ExtendedCSharpCodeProvider(ProxyGenerator<TContract>.CreateGeneratedProxyCode(contractObjectModel, proxyGenerationOptions), list.ToArray(), "").BuildAssembly(ExtendedCSharpCodeProviderBuildOptions.None, null), contractObjectModel.Namespace, contractObjectModel.GeneratedName, false);
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00087B34 File Offset: 0x00085D34
		private static string CreateGeneratedProxyCode(ContractObjectModel com, ProxyGenerationOptions proxyGenerationOptions)
		{
			ExtendedStringBuilder extendedStringBuilder = new ExtendedStringBuilder();
			ProxyGenerator<TContract>.WriteNamespace(com, extendedStringBuilder);
			ProxyGenerator<TContract>.WriteClassCode(com, extendedStringBuilder, proxyGenerationOptions);
			extendedStringBuilder.RemoveTabFromPrefix(true);
			return extendedStringBuilder.ToString();
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x00087B64 File Offset: 0x00085D64
		private static void WriteClassCode(ContractObjectModel com, ExtendedStringBuilder sb, ProxyGenerationOptions proxyGenerationOptions)
		{
			string text = "{0}{1}".FormatWithCurrentCulture(new object[] { com.GeneratedName, "Proxy" });
			string text2 = com.FullName;
			if (com.CreateInternalInterface)
			{
				text2 = (string.IsNullOrEmpty(com.ServiceContractName) ? "{0}.ECFProxies.{1}".FormatWithCurrentCulture(new object[] { com.Namespace, com.Name }) : "{0}.ECFProxies.{1}".FormatWithCurrentCulture(new object[] { com.Namespace, com.ServiceContractName }));
				ProxyGenerator<TContract>.GenerateNewInterface(com, string.IsNullOrEmpty(com.ServiceContractName) ? com.Name : com.ServiceContractName, sb);
			}
			sb.AppendLine("public sealed class {0}: {1}".FormatWithCurrentCulture(new object[] { text, com.FullName }));
			sb.AppendTabToPrefix(true);
			sb.AppendLine("private IProxyInvoker<{0}> m_invoker;".FormatWithCurrentCulture(new object[] { text2 }));
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine("public {0}(object proxyInvoker)".FormatWithCurrentCulture(new object[] { text }));
			sb.AppendTabToPrefix(true);
			sb.AppendLine("m_invoker = proxyInvoker as IProxyInvoker<{0}> ;".FormatWithCurrentCulture(new object[] { text2 }));
			sb.AppendLine("ExtendedDiagnostics.EnsureNotNull(m_invoker, \"m_invoker\");");
			sb.RemoveTabFromPrefix(true);
			sb.AppendLine();
			ProxyGenerator<TContract>.WriteMethods(com, sb, proxyGenerationOptions);
			ProxyGenerator<TContract>.WritePrivateMethods(com, sb, proxyGenerationOptions);
			sb.RemoveTabFromPrefix(true);
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x00087CD4 File Offset: 0x00085ED4
		private static void GenerateNewInterface(ContractObjectModel com, string proxyName, ExtendedStringBuilder sb)
		{
			sb.AppendLine("[ServiceContract{0}]".FormatWithCurrentCulture(new object[] { ProxyGenerator<TContract>.GetServiceContractParams(com) }));
			sb.AppendLine("[ECFGeneratedInterface(OriginalInterface = \"{0}\")]".FormatWithCurrentCulture(new object[] { com.FullName }));
			ProxyGenerator<TContract>.AddInterfaceAttributes(com, sb);
			sb.AppendLine("internal interface {0}".FormatWithCurrentCulture(new object[] { proxyName }));
			sb.AppendTabToPrefix(true);
			foreach (ContractMethod contractMethod in com.Methods)
			{
				string text = string.Empty;
				text += ProxyGenerator<TContract>.FormatMethodParameters(contractMethod.Method.GetParameters(), contractMethod.RemovableParameters, true, false, false, false, false, true);
				string text2 = (contractMethod.UseExtendedResult ? ProxyGenerator<TContract>.GetContractMethodReturnType(contractMethod.InnerResultType) : ProxyGenerator<TContract>.GetMethodReturnType(contractMethod));
				ProxyGenerator<TContract>.AddOperationAttributes(contractMethod, sb);
				sb.AppendLine("{0} {1}({2});".FormatWithCurrentCulture(new object[]
				{
					text2,
					contractMethod.Method.Name,
					text
				}));
				sb.AppendLine();
			}
			sb.RemoveTabFromPrefix(true);
			sb.AppendLine();
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x00087E14 File Offset: 0x00086014
		private static void AddOperationAttributes(ContractMethod method, ExtendedStringBuilder sb)
		{
			foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(method.Method))
			{
				string text = customAttributeData.ToString();
				text = text.Replace("= True", "= true");
				sb.AppendLine(text);
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x00087E7C File Offset: 0x0008607C
		private static void AddInterfaceAttributes(ContractObjectModel com, ExtendedStringBuilder sb)
		{
			foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(com.Contract))
			{
				if (customAttributeData.Constructor.DeclaringType.FullName != typeof(ECFContractAttribute).FullName && customAttributeData.Constructor.DeclaringType.FullName != typeof(ServiceContractAttribute).FullName)
				{
					sb.AppendLine(customAttributeData.ToString());
				}
			}
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x00087F20 File Offset: 0x00086120
		private static string GetServiceContractParams(ContractObjectModel com)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			if (!string.IsNullOrEmpty(com.ServiceContractName))
			{
				stringBuilder.Append("Name=\"");
				stringBuilder.Append(com.ServiceContractName);
				stringBuilder.Append("\"");
			}
			if (!string.IsNullOrEmpty(com.ServiceContractNamespace))
			{
				if (stringBuilder.Length > 1)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Namespace=\"");
				stringBuilder.Append(com.ServiceContractNamespace);
				stringBuilder.Append("\"");
			}
			stringBuilder.Append(")");
			if (stringBuilder.Length == 2)
			{
				return string.Empty;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x00087FDC File Offset: 0x000861DC
		private static void WriteMethods(ContractObjectModel com, ExtendedStringBuilder sb, ProxyGenerationOptions proxyGenerationOptions)
		{
			foreach (ContractMethod contractMethod in com.Methods)
			{
				switch (contractMethod.MethodType)
				{
				case ContractMethodType.OperationContractSync:
					ProxyGenerator<TContract>.WriteSyncProxyCode(contractMethod, com, sb, proxyGenerationOptions);
					sb.AppendLine();
					break;
				case ContractMethodType.OperationContractBegin:
					ProxyGenerator<TContract>.WriteBeginProxyCode(contractMethod, com, sb, proxyGenerationOptions);
					sb.AppendLine();
					break;
				case ContractMethodType.OperationContractEnd:
					ProxyGenerator<TContract>.WriteEndProxyCode(contractMethod, com, sb);
					break;
				case ContractMethodType.NotOperationContract:
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "method '{0}' is not decorated with an OperationContract attribute", new object[] { contractMethod.Method.Name }));
				case ContractMethodType.OperationContractTaskAsync:
					ProxyGenerator<TContract>.WriteTaskAsyncProxyCode(contractMethod, com, sb, proxyGenerationOptions);
					sb.AppendLine();
					break;
				}
				sb.AppendLine();
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000880B8 File Offset: 0x000862B8
		private static void WritePrivateMethods(ContractObjectModel com, ExtendedStringBuilder sb, ProxyGenerationOptions proxyGenerationOptions)
		{
			if (proxyGenerationOptions.HasFlag(ProxyGenerationOptions.TraceHttpResponseHeadersOnFaultException))
			{
				sb.AppendLine();
				ProxyGenerator<TContract>.GenerateTraceHttpResponseHeadersPrivateMethod(sb);
			}
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000880DC File Offset: 0x000862DC
		private static string FormatMethodKeys(ICollection<ECFParameter> keys, bool withType, bool endWithComma, bool asProperties)
		{
			ParameterInfo[] array = new ParameterInfo[keys.Count];
			int num = 0;
			foreach (ECFParameter ecfparameter in keys)
			{
				array[num] = ecfparameter.Parameter;
				num++;
			}
			return ProxyGenerator<TContract>.FormatMethodParameters(array, keys, withType, endWithComma, asProperties, false);
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x00088144 File Offset: 0x00086344
		private static string InsertEcfHttpHeaders(IEnumerable<ECFParameter> headers)
		{
			if (headers.Count<ECFParameter>() == 0)
			{
				return "null";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("new List<EcfHttpMessageHeader>() {");
			foreach (ECFParameter ecfparameter in headers)
			{
				string text = ExtendedReflection.GetParameterValueFromCustomAttribute(ExtendedReflection.GetCustomAttribute(CustomAttributeData.GetCustomAttributes(ecfparameter.Parameter), typeof(ECFHttpHeaderAttribute).FullName), "HeaderName") as string;
				stringBuilder.Append("new EcfHttpMessageHeader(\"{0}\", {1}), ".FormatWithCurrentCulture(new object[]
				{
					text,
					ecfparameter.Parameter.Name
				}));
			}
			stringBuilder.Remove(stringBuilder.Length - 2, 2);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x00088220 File Offset: 0x00086420
		private static string InsertEcfSoapHeaders(IEnumerable<ECFParameter> headers)
		{
			if (headers.Count<ECFParameter>() == 0)
			{
				return "null";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("new List<EcfSoapMessageHeader>() {");
			foreach (ECFParameter ecfparameter in headers)
			{
				CustomAttributeData customAttribute = ExtendedReflection.GetCustomAttribute(CustomAttributeData.GetCustomAttributes(ecfparameter.Parameter), typeof(ECFSoapHeaderAttribute).FullName);
				string text = ExtendedReflection.GetParameterValueFromCustomAttribute(customAttribute, "HeaderName") as string;
				string text2 = ExtendedReflection.GetParameterValueFromCustomAttribute(customAttribute, "HeaderNamespace") as string;
				stringBuilder.Append("new EcfSoapMessageHeader(\"{0}\", \"{1}\", {2}), ".FormatWithCurrentCulture(new object[]
				{
					text,
					text2,
					ecfparameter.Parameter.Name
				}));
			}
			stringBuilder.Remove(stringBuilder.Length - 2, 2);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00088314 File Offset: 0x00086514
		private static void WriteSyncProxyCode(ContractMethod contractMethod, ContractObjectModel com, ExtendedStringBuilder sb, ProxyGenerationOptions proxyGenerationOptions)
		{
			string methodReturnType = ProxyGenerator<TContract>.GetMethodReturnType(contractMethod);
			ProxyGenerator<TContract>.WriteMethodName(contractMethod, true, sb);
			sb.AppendTabToPrefix(true);
			if (contractMethod.MethodReturnValue == MethodReturnValue.NonVoid)
			{
				sb.AppendLine("var ECFResult = default({0});".FormatWithCurrentCulture(new object[] { methodReturnType }));
				sb.AppendLine("m_invoker.EndInvoke(");
			}
			else
			{
				sb.AppendLine("m_invoker.EndInvoke(");
			}
			sb.AppendLine("        m_invoker.BeginInvoke(\"{0}\", new object[] {{ {1} }}, {2}, {3},".FormatWithCurrentCulture(new object[]
			{
				contractMethod.Method.Name,
				(contractMethod.BalancingKeys.Count > 0) ? ProxyGenerator<TContract>.FormatMethodKeys(contractMethod.BalancingKeys, false, false, false) : "",
				ProxyGenerator<TContract>.InsertEcfHttpHeaders(contractMethod.HeaderHttpParameters),
				ProxyGenerator<TContract>.InsertEcfSoapHeaders(contractMethod.HeaderSoapParameters)
			}));
			ProxyGenerator<TContract>.WriteDelegates(contractMethod, com, sb, proxyGenerationOptions);
			sb.AppendLine("null, null));");
			if (contractMethod.MethodReturnValue == MethodReturnValue.NonVoid)
			{
				sb.AppendLine("return ECFResult;");
			}
			sb.RemoveTabFromPrefix(true);
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00088408 File Offset: 0x00086608
		private static void WriteMethodName(ContractMethod contractMethod, bool mainMethod, ExtendedStringBuilder sb)
		{
			MethodInfo method = contractMethod.Method;
			string text = string.Empty;
			string text2 = (mainMethod ? string.Empty : "_inner");
			text += ProxyGenerator<TContract>.FormatMethodParameters(method.GetParameters(), contractMethod.BalancingKeys, true, false, false, false);
			string methodReturnType = ProxyGenerator<TContract>.GetMethodReturnType(contractMethod);
			if (contractMethod.MethodReturnValue == MethodReturnValue.Void)
			{
				sb.AppendLine("public void {0}{1}({2})".FormatWithCurrentCulture(new object[] { method.Name, text2, text }));
				return;
			}
			sb.AppendLine("public {0} {1}{2}({3})".FormatWithCurrentCulture(new object[] { methodReturnType, method.Name, text2, text }));
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000884B0 File Offset: 0x000866B0
		private static bool IsRemovableParams(ParameterInfo parameter, IEnumerable<ECFParameter> balancingKeys)
		{
			return balancingKeys != null && balancingKeys.Count<ECFParameter>() != 0 && balancingKeys.Where((ECFParameter k) => k.Parameter.Name.Equals(parameter.Name) && k.Remove).Any<ECFParameter>();
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000884EE File Offset: 0x000866EE
		private static string FormatMethodParameters(ParameterInfo[] parameters, IEnumerable<ECFParameter> balancingKeys, bool withType, bool endWithComma, bool asProperties, bool withoutAsyncParameters)
		{
			return ProxyGenerator<TContract>.FormatMethodParameters(parameters, balancingKeys, withType, endWithComma, asProperties, withoutAsyncParameters, true, false);
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00088500 File Offset: 0x00086700
		private static string FormatMethodParameters(ParameterInfo[] parameters, IEnumerable<ECFParameter> balancingKeys, bool withType, bool endWithComma, bool asProperties, bool withoutAsyncParameters, bool ignoreRemovableKeys, bool addParameterAttribute)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (parameters != null && parameters.Length != 0)
			{
				int num = 1;
				foreach (ParameterInfo parameterInfo in parameters)
				{
					if (ignoreRemovableKeys || !ProxyGenerator<TContract>.IsRemovableParams(parameterInfo, balancingKeys))
					{
						if (withoutAsyncParameters && num == parameters.Count<ParameterInfo>() - 1)
						{
							break;
						}
						if (addParameterAttribute)
						{
							foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(parameterInfo))
							{
								stringBuilder.Append(customAttributeData.ToString());
							}
						}
						if (withType)
						{
							if (parameterInfo.ParameterType.IsGenericType)
							{
								stringBuilder.Append(ProxyGenerator<TContract>.ConvertGenericParameter(parameterInfo.ParameterType));
							}
							else
							{
								stringBuilder.Append(parameterInfo.ParameterType + " ");
							}
						}
						if (asProperties)
						{
							stringBuilder.Append(ProxyGenerator<TContract>.ConvertParameterToProperty(parameterInfo.Name));
						}
						else
						{
							stringBuilder.Append(parameterInfo.Name);
						}
						stringBuilder.Append(", ");
					}
					num++;
				}
				if (!endWithComma && stringBuilder.Length >= 2)
				{
					stringBuilder.Remove(stringBuilder.Length - 2, 2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x0008864C File Offset: 0x0008684C
		private static string ConvertParameterToProperty(string name)
		{
			return name.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture) + name.Substring(1, name.Length - 1);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x00088674 File Offset: 0x00086874
		private static string GetReturnType(ContractMethod contractMethod, ContractObjectModel com)
		{
			Type type = null;
			bool flag = false;
			if (contractMethod.MethodType == ContractMethodType.OperationContractBegin)
			{
				ContractMethod endContractMethod = ContractObjectModel.GetEndContractMethod(contractMethod, com);
				if (endContractMethod.MethodReturnValue == MethodReturnValue.NonVoid)
				{
					flag = true;
					type = endContractMethod.Method.ReturnType;
				}
			}
			if ((contractMethod.MethodType == ContractMethodType.OperationContractSync || contractMethod.MethodType == ContractMethodType.OperationContractEnd) && contractMethod.MethodReturnValue == MethodReturnValue.NonVoid)
			{
				flag = true;
				type = contractMethod.Method.ReturnType;
			}
			if (flag)
			{
				return ProxyGenerator<TContract>.ConvertGenericParameter(type);
			}
			return "void";
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000886E4 File Offset: 0x000868E4
		private static void WriteBeginProxyCode(ContractMethod contractMethod, ContractObjectModel com, ExtendedStringBuilder sb, ProxyGenerationOptions proxyGenerationOptions)
		{
			ParameterInfo[] parameters = contractMethod.Method.GetParameters();
			string text = parameters[parameters.Count<ParameterInfo>() - 2].Name + ", " + parameters[parameters.Count<ParameterInfo>() - 1].Name;
			ProxyGenerator<TContract>.WriteMethodName(contractMethod, true, sb);
			sb.AppendTabToPrefix(true);
			string returnType = ProxyGenerator<TContract>.GetReturnType(contractMethod, com);
			sb.AppendLine("var car = new  ChainedAsyncResult<WorkTicket {0}({1}, null{2};".FormatWithCurrentCulture(new object[]
			{
				returnType.Equals("void") ? ">" : (", " + returnType + ">"),
				text,
				returnType.Equals("void") ? ");" : (", default(" + returnType + "));")
			}));
			sb.AppendLine("car.InnerResult =  m_invoker.BeginInvoke(\"{0}\", new object[] {{ {1} }}, {2}, {3},".FormatWithCurrentCulture(new object[]
			{
				contractMethod.Method.Name,
				(contractMethod.BalancingKeys.Count > 0) ? ProxyGenerator<TContract>.FormatMethodKeys(contractMethod.BalancingKeys, false, false, false) : "",
				ProxyGenerator<TContract>.InsertEcfHttpHeaders(contractMethod.HeaderHttpParameters),
				ProxyGenerator<TContract>.InsertEcfSoapHeaders(contractMethod.HeaderSoapParameters)
			}));
			ProxyGenerator<TContract>.WriteDelegates(contractMethod, com, sb, proxyGenerationOptions);
			sb.AppendLine("car.BeginAsyncFunctionCallback, null);");
			sb.AppendLine();
			sb.AppendLine("return car;");
			sb.RemoveTabFromPrefix(true);
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x00088838 File Offset: 0x00086A38
		private static void WriteEndProxyCode(ContractMethod contractMethod, ContractObjectModel com, ExtendedStringBuilder sb)
		{
			ParameterInfo[] parameters = contractMethod.Method.GetParameters();
			ProxyGenerator<TContract>.WriteMethodName(contractMethod, true, sb);
			sb.AppendTabToPrefix(true);
			string returnType = ProxyGenerator<TContract>.GetReturnType(contractMethod, com);
			sb.AppendLine("var car = (ChainedAsyncResult<WorkTicket{0}{1};".FormatWithCurrentCulture(new object[]
			{
				returnType.Equals("void") ? ">)" : (", " + returnType + ">)"),
				parameters[parameters.Count<ParameterInfo>() - 1].Name
			}));
			sb.AppendLine("car.End();");
			sb.AppendLine("m_invoker.EndInvoke(car.InnerResult);");
			if (!returnType.Equals("void"))
			{
				sb.AppendLine("return car.Data;");
			}
			sb.RemoveTabFromPrefix(true);
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000888EC File Offset: 0x00086AEC
		private static void WriteTaskAsyncProxyCode(ContractMethod contractMethod, ContractObjectModel com, ExtendedStringBuilder sb, ProxyGenerationOptions proxyGenerationOptions)
		{
			ProxyGenerator<TContract>.WriteMethodName(contractMethod, true, sb);
			sb.AppendTabToPrefix(true);
			sb.AppendLine(string.Format(CultureInfo.CurrentCulture, "return m_invoker.InvokeAsync(\"{0}\", new object[] {{ {1} }}, {2}, {3},", new object[]
			{
				contractMethod.Method.Name,
				(contractMethod.BalancingKeys.Count > 0) ? ProxyGenerator<TContract>.FormatMethodKeys(contractMethod.BalancingKeys, false, false, false) : "",
				ProxyGenerator<TContract>.InsertEcfHttpHeaders(contractMethod.HeaderHttpParameters),
				ProxyGenerator<TContract>.InsertEcfSoapHeaders(contractMethod.HeaderSoapParameters)
			}));
			string text = ProxyGenerator<TContract>.FormatMethodParameters(contractMethod.Method.GetParameters(), contractMethod.RemovableParameters, false, false, false, false, proxyGenerationOptions.HasFlag(ProxyGenerationOptions.IgnoreRemovableParams), false);
			sb.AppendLine("delegate(" + ProxyGenerator<TContract>.GetChannelName(com) + " innerProxy) {");
			sb.AppendTabToPrefix(false);
			sb.AppendLine(string.Format(CultureInfo.CurrentCulture, "return innerProxy.{0}({1});", new object[]
			{
				contractMethod.Method.Name,
				text
			}));
			sb.RemoveTabFromPrefix(false);
			sb.AppendLine("});");
			sb.RemoveTabFromPrefix(true);
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x00088A0E File Offset: 0x00086C0E
		private static void WriteDelegates(ContractMethod contractMethod, ContractObjectModel com, ExtendedStringBuilder sb, ProxyGenerationOptions proxyGenerationOptions)
		{
			ProxyGenerator<TContract>.WriteBeginExecuteDelegate(contractMethod, com, sb, proxyGenerationOptions);
			sb.AppendLine();
			ProxyGenerator<TContract>.WriteEndExecuteDelegate(contractMethod, com, sb, proxyGenerationOptions);
			sb.AppendLine();
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x00088A30 File Offset: 0x00086C30
		private static string GetChannelName(ContractObjectModel com)
		{
			string text = com.FullName;
			if (com.CreateInternalInterface)
			{
				text = "{0}.{1}.{2}".FormatWithCurrentCulture(new object[]
				{
					com.Namespace,
					"ECFProxies",
					string.IsNullOrEmpty(com.ServiceContractName) ? com.Name : com.ServiceContractName
				});
			}
			return text;
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x00088A90 File Offset: 0x00086C90
		private static void WriteEndExecuteDelegate(ContractMethod contractMethod, ContractObjectModel com, ExtendedStringBuilder sb, ProxyGenerationOptions proxyGenerationOptions)
		{
			bool flag = contractMethod.MethodType > ContractMethodType.OperationContractSync;
			sb.AppendLine("delegate(IAsyncResult innerAr) {");
			sb.AppendTabToPrefix(false);
			if (flag)
			{
				ContractMethod correspondingContractMethod = ContractObjectModel.GetEndContractMethod(contractMethod, com);
				sb.AppendLine("var innerCar = (ChainedAsyncResult<WorkTicket, {0}>)innerAr;".FormatWithCurrentCulture(new object[] { ProxyGenerator<TContract>.GetChannelName(com) }));
				sb.AppendLine("innerCar.End();");
				sb.AppendLine("{0} ecfProxy = innerCar.Data;".FormatWithCurrentCulture(new object[] { ProxyGenerator<TContract>.GetChannelName(com) }));
				ProxyGenerator<TContract>.ExceptionHandlingGeneration(proxyGenerationOptions.HasFlag(ProxyGenerationOptions.TraceHttpResponseHeadersOnFaultException), sb, delegate
				{
					if (correspondingContractMethod.MethodReturnValue != MethodReturnValue.NonVoid)
					{
						sb.AppendLine("ecfProxy.{0}(innerCar.InnerResult);".FormatWithCurrentCulture(new object[] { correspondingContractMethod.Method.Name }));
						return;
					}
					if (correspondingContractMethod.UseExtendedResult)
					{
						string text = "ecfProxy.{0}(innerCar.InnerResult)".FormatWithCurrentCulture(new object[] { correspondingContractMethod.Method.Name }) + ((proxyGenerationOptions.HasFlag(ProxyGenerationOptions.IgnoreRemovableParams) && !string.IsNullOrWhiteSpace(correspondingContractMethod.InnerResultPropertyName)) ? ".{0}".FormatWithCurrentCulture(new object[] { correspondingContractMethod.InnerResultPropertyName }) : string.Empty);
						ProxyGenerator<TContract>.GenerateEcfExtendedResult(contractMethod.MethodType, ProxyGenerator<TContract>.GetReturnType(correspondingContractMethod, com), correspondingContractMethod.InnerResultPropertyName, text, correspondingContractMethod.PropertyToHeaderDictionary, sb);
						sb.AppendLine("car.Data = extendedResult;");
						return;
					}
					sb.AppendLine("car.Data = ecfProxy.{0}(innerCar.InnerResult);".FormatWithCurrentCulture(new object[] { correspondingContractMethod.Method.Name }));
				}, new Dictionary<Type, Action> { 
				{
					typeof(FaultException),
					delegate
					{
						sb.AppendLine("TraceHttpResponseHeaders();");
						sb.AppendLine("throw;");
					}
				} });
			}
			else
			{
				string methodInnerReturnType = ProxyGenerator<TContract>.GetMethodInnerReturnType(contractMethod);
				sb.AppendLine("var car = (CompletedAsyncResult{0})innerAr;".FormatWithCurrentCulture(new object[] { methodInnerReturnType.Equals("void") ? "" : ("<" + methodInnerReturnType + ">") }));
				sb.AppendLine("car.End();");
			}
			sb.RemoveTabFromPrefix(false);
			sb.AppendLine("},");
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x00088C70 File Offset: 0x00086E70
		private static void WriteBeginExecuteDelegate(ContractMethod contractMethod, ContractObjectModel com, ExtendedStringBuilder sb, ProxyGenerationOptions proxyGenerationOptions)
		{
			bool flag = contractMethod.MethodType == ContractMethodType.OperationContractBegin;
			string parameters = ProxyGenerator<TContract>.FormatMethodParameters(contractMethod.Method.GetParameters(), contractMethod.RemovableParameters, false, false, false, flag, proxyGenerationOptions.HasFlag(ProxyGenerationOptions.IgnoreRemovableParams), false);
			sb.AppendLine("delegate(" + ProxyGenerator<TContract>.GetChannelName(com) + " innerProxy, AsyncCallback innerCb, Object innerState) {");
			sb.AppendTabToPrefix(false);
			if (flag)
			{
				sb.AppendLine("var innerCar =  new ChainedAsyncResult<WorkTicket, {0}>(innerCb, innerState, null, innerProxy);".FormatWithCurrentCulture(new object[] { ProxyGenerator<TContract>.GetChannelName(com) }));
				sb.AppendLine("innerCar.InnerResult = innerProxy.{0}({1}{2} innerCar.BeginAsyncFunctionCallback, null); ".FormatWithCurrentCulture(new object[]
				{
					contractMethod.Method.Name,
					parameters,
					string.IsNullOrEmpty(parameters) ? "" : ","
				}));
				sb.AppendLine("return innerCar; },");
			}
			else
			{
				string methodInnerReturnType = ProxyGenerator<TContract>.GetMethodInnerReturnType(contractMethod);
				if (ProxyGenerator<TContract>.HasReturnValue(contractMethod))
				{
					sb.AppendLine("var result = default({0});".FormatWithCurrentCulture(new object[] { methodInnerReturnType }));
				}
				ProxyGenerator<TContract>.ExceptionHandlingGeneration(proxyGenerationOptions.HasFlag(ProxyGenerationOptions.TraceHttpResponseHeadersOnFaultException), sb, delegate
				{
					if (ProxyGenerator<TContract>.HasReturnValue(contractMethod))
					{
						sb.AppendLine("result = innerProxy.{0}({1});".FormatWithInvariantCulture(new object[]
						{
							contractMethod.Method.Name,
							parameters
						}));
						return;
					}
					sb.AppendLine("innerProxy.{0}({1});".FormatWithCurrentCulture(new object[]
					{
						contractMethod.Method.Name,
						parameters
					}));
				}, new Dictionary<Type, Action> { 
				{
					typeof(FaultException),
					delegate
					{
						sb.AppendLine("TraceHttpResponseHeaders();");
						sb.AppendLine("throw;");
					}
				} });
				sb.AppendLine("var car = new CompletedAsyncResult{0}(innerCb, innerState{1}".FormatWithCurrentCulture(new object[]
				{
					(!ProxyGenerator<TContract>.HasReturnValue(contractMethod)) ? "" : ("<" + methodInnerReturnType + ">"),
					(!ProxyGenerator<TContract>.HasReturnValue(contractMethod)) ? ");" : ", result);"
				}));
				if (contractMethod.MethodReturnValue == MethodReturnValue.NonVoid)
				{
					if (contractMethod.UseExtendedResult)
					{
						ProxyGenerator<TContract>.GenerateEcfExtendedResult(contractMethod.MethodType, ProxyGenerator<TContract>.GetMethodReturnType(contractMethod), contractMethod.InnerResultPropertyName, "result", contractMethod.PropertyToHeaderDictionary, sb);
						sb.AppendLine("ECFResult = extendedResult;");
					}
					else
					{
						sb.AppendLine("ECFResult = result;");
					}
				}
				sb.AppendLine("return car; },");
			}
			sb.RemoveTabFromPrefix(false);
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x00088F1A File Offset: 0x0008711A
		private static bool HasReturnValue(ContractMethod contractMethod)
		{
			if (contractMethod.UseExtendedResult)
			{
				return !string.IsNullOrEmpty(contractMethod.InnerResultPropertyName);
			}
			return contractMethod.MethodReturnValue == MethodReturnValue.NonVoid;
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x00088F3C File Offset: 0x0008713C
		private static string GetMethodInnerReturnType(ContractMethod contractMethod)
		{
			if (!contractMethod.UseExtendedResult)
			{
				return ProxyGenerator<TContract>.GetMethodReturnType(contractMethod);
			}
			return ProxyGenerator<TContract>.GetContractMethodReturnType(contractMethod.InnerResultType);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x00088F58 File Offset: 0x00087158
		private static string GetMethodReturnType(ContractMethod contractMethod)
		{
			if (contractMethod.MethodReturnValue == MethodReturnValue.Void)
			{
				return "void";
			}
			return ProxyGenerator<TContract>.GetContractMethodReturnType(contractMethod.Method.ReturnType);
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x00088F78 File Offset: 0x00087178
		private static string GetContractMethodReturnType(Type type)
		{
			if (type == null)
			{
				return "void";
			}
			if (type.IsGenericType)
			{
				return ProxyGenerator<TContract>.ConvertGenericParameter(type);
			}
			return type.ToString();
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x00088FA0 File Offset: 0x000871A0
		private static string ConvertGenericParameter(Type parameterType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type[] genericArguments = parameterType.GetGenericArguments();
			if (genericArguments.Length != 0)
			{
				string text = parameterType.ToString();
				int num = text.IndexOf('`');
				stringBuilder.Append(text.Substring(0, num));
				stringBuilder.Append("<");
				foreach (Type type in genericArguments)
				{
					stringBuilder.Append(ProxyGenerator<TContract>.ConvertGenericParameter(type));
					stringBuilder.Append(", ");
				}
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
				stringBuilder.Append("> ");
				return stringBuilder.ToString();
			}
			return parameterType.ToString();
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x0008904C File Offset: 0x0008724C
		private static void WriteNamespace(ContractObjectModel com, ExtendedStringBuilder sb)
		{
			sb.AppendLine("// ----------------------------------------------------------------------------");
			sb.AppendLine("// Copyright (c) Microsoft Corporation. All rights reserved.");
			sb.AppendLine("// This class is an auto-generated ECF proxy");
			sb.AppendLine("// Creation Date: {0}".FormatWithInvariantCulture(new object[] { DateTime.UtcNow }));
			sb.AppendLine("// ----------------------------------------------------------------------------");
			sb.AppendLine();
			sb.AppendLine("using System;");
			sb.AppendLine("using System.Collections.Generic;");
			sb.AppendLine("using System.Diagnostics;");
			sb.AppendLine("using System.ServiceModel;");
			sb.AppendLine("using System.ServiceModel.Channels;");
			sb.AppendLine("using System.Text;");
			sb.AppendLine("using Microsoft.Cloud.Platform.Communication;");
			sb.AppendLine("using Microsoft.Cloud.Platform.Utils;");
			sb.AppendLine("using Microsoft.Cloud.Platform.CommunicationFramework.Attributes;");
			sb.AppendLine();
			sb.AppendLine("namespace {0}.{1}".FormatWithCurrentCulture(new object[] { com.Namespace, "ECFProxies" }));
			sb.AppendTabToPrefix(true);
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x00089148 File Offset: 0x00087348
		private static void GenerateEcfExtendedResult(ContractMethodType methodType, string resultTypeName, string innerResultPropertyName, string innerResultExpression, Dictionary<string, string> propertyToHeaderDictionary, ExtendedStringBuilder sb)
		{
			sb.AppendLine("// Create ECF extended result");
			sb.AppendLine("var extendedResult = new {0}();".FormatWithInvariantCulture(new object[] { resultTypeName }));
			if (!string.IsNullOrEmpty(innerResultPropertyName))
			{
				sb.AppendLine("extendedResult.{0} = {1};".FormatWithInvariantCulture(new object[] { innerResultPropertyName, innerResultExpression }));
			}
			else if (methodType != ContractMethodType.OperationContractSync)
			{
				sb.AppendLine(innerResultExpression + ";");
			}
			sb.AppendLine();
			sb.AppendLine("// Fill in http response headers");
			sb.AppendLine("if(OperationContext.Current != null)");
			sb.AppendTabToPrefix(true);
			sb.AppendLine("var httpResponseMessageProperty = OperationContext.Current.IncomingMessageProperties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;");
			foreach (string text in propertyToHeaderDictionary.Keys)
			{
				string text2;
				if (propertyToHeaderDictionary.TryGetValue(text, out text2))
				{
					sb.AppendLine("extendedResult.{0} = httpResponseMessageProperty.Headers[\"{1}\"];".FormatWithInvariantCulture(new object[] { text, text2 }));
				}
			}
			sb.RemoveTabFromPrefix(true);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x00089260 File Offset: 0x00087460
		private static void ExceptionHandlingGeneration(bool shouldHandleExceptions, ExtendedStringBuilder sb, Action innerCodeGenerationAction, [NotNull] Dictionary<Type, Action> exceptionToActionMapper)
		{
			ExtendedDiagnostics.EnsureEnumerableNotNullOrEmpty<KeyValuePair<Type, Action>>(exceptionToActionMapper, "exceptionToActionMapper");
			if (shouldHandleExceptions)
			{
				sb.AppendLine("try");
				sb.AppendTabToPrefix(true);
				innerCodeGenerationAction();
				sb.RemoveTabFromPrefix(true);
				using (Dictionary<Type, Action>.KeyCollection.Enumerator enumerator = exceptionToActionMapper.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Type type = enumerator.Current;
						ExtendedDiagnostics.EnsureArgument("exceptionToActionMapper", typeof(Exception).IsAssignableFrom(type), "{0} is not a valid exception type".FormatWithInvariantCulture(new object[] { type }));
						sb.AppendLine("catch ({0})".FormatWithInvariantCulture(new object[] { type }));
						sb.AppendTabToPrefix(true);
						exceptionToActionMapper[type]();
						sb.RemoveTabFromPrefix(true);
					}
					return;
				}
			}
			innerCodeGenerationAction();
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x00089344 File Offset: 0x00087544
		private static void GenerateTraceHttpResponseHeadersPrivateMethod(ExtendedStringBuilder esb)
		{
			esb.AppendLine("private void TraceHttpResponseHeaders()");
			esb.AppendTabToPrefix(true);
			esb.AppendLine("if (OperationContext.Current != null && OperationContext.Current.IncomingMessageProperties.ContainsKey(HttpResponseMessageProperty.Name))");
			esb.AppendTabToPrefix(true);
			esb.AppendLine("var httpResponseProperties = OperationContext.Current.IncomingMessageProperties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;");
			esb.AppendLine("var sb = new StringBuilder().AppendLine(\"Http Response Headers:\");");
			esb.AppendLine("foreach (var key in httpResponseProperties.Headers.AllKeys)");
			esb.AppendTabToPrefix(true);
			esb.AppendLine("sb.AppendLine(\"{0}: {1};\".FormatWithInvariantCulture(key, httpResponseProperties.Headers[key]));");
			esb.RemoveTabFromPrefix(true);
			esb.AppendLine("CommunicationFrameworkTrace.Tracer.TraceInformation(sb.ToString());");
			esb.RemoveTabFromPrefix(true);
			esb.RemoveTabFromPrefix(true);
		}

		// Token: 0x04000DA3 RID: 3491
		internal const string c_proxyNamespaceSuffix = "ECFProxies";

		// Token: 0x04000DA4 RID: 3492
		internal const string c_proxyNameSuffix = "Proxy";
	}
}
