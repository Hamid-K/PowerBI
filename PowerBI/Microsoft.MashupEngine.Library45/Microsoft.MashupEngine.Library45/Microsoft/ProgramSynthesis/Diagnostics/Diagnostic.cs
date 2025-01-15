using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Diagnostics
{
	// Token: 0x0200088D RID: 2189
	public abstract class Diagnostic : IEquatable<Diagnostic>
	{
		// Token: 0x06002FEB RID: 12267 RVA: 0x0008DC70 File Offset: 0x0008BE70
		private Diagnostic(string id, GrammarValidation category, Severity severity, string messageFormat, Location location, params object[] arguments)
		{
			this._id = id;
			this._category = category;
			this._severity = severity;
			this._messageFormat = messageFormat;
			this.Location = location;
			this._arguments = arguments;
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x0008DCA5 File Offset: 0x0008BEA5
		public Location Location { get; }

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x0008DCAD File Offset: 0x0008BEAD
		public GrammarValidation Category
		{
			get
			{
				return this._category;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x0008DCB5 File Offset: 0x0008BEB5
		public string ID
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002FEF RID: 12271 RVA: 0x0008DCC0 File Offset: 0x0008BEC0
		public string Message
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "[{3}] {0} {1}: {2}", new object[]
				{
					this.Severity,
					this.ID,
					string.Format(CultureInfo.InvariantCulture, this._messageFormat, this._arguments),
					(this.Location == null) ? "" : this.Location.Message
				});
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002FF0 RID: 12272 RVA: 0x0008DD2F File Offset: 0x0008BF2F
		public Severity Severity
		{
			get
			{
				return this._severity;
			}
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x0008DD38 File Offset: 0x0008BF38
		public bool Equals(Diagnostic other)
		{
			return other != null && (this == other || (string.Equals(this._id, other._id) && this._category == other._category && this._severity == other._severity && string.Equals(this._messageFormat, other._messageFormat) && ValueEquality.Comparer.Equals(this._arguments, other._arguments)));
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x0008DDAA File Offset: 0x0008BFAA
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Diagnostic)obj)));
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x0008DDD8 File Offset: 0x0008BFD8
		public override int GetHashCode()
		{
			string id = this._id;
			int num = ((((((id != null) ? id.GetHashCode() : 0) * 13949449) ^ (int)this._category) * 13949449) ^ (int)this._severity) * 13949449;
			string messageFormat = this._messageFormat;
			return ((num ^ ((messageFormat != null) ? messageFormat.GetHashCode() : 0)) * 13949449) ^ ((this._arguments != null) ? ValueEquality.Comparer.GetHashCode(this._arguments) : 0);
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(Diagnostic left, Diagnostic right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(Diagnostic left, Diagnostic right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x0008DE4C File Offset: 0x0008C04C
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x04001775 RID: 6005
		private readonly string _id;

		// Token: 0x04001776 RID: 6006
		private readonly GrammarValidation _category;

		// Token: 0x04001777 RID: 6007
		private readonly Severity _severity;

		// Token: 0x04001778 RID: 6008
		private readonly string _messageFormat;

		// Token: 0x04001779 RID: 6009
		private readonly object[] _arguments;

		// Token: 0x0200088E RID: 2190
		[DebuggerNonUserCode]
		internal sealed class Core_ReferenceNotFound : Diagnostic
		{
			// Token: 0x06002FF7 RID: 12279 RVA: 0x0008DE54 File Offset: 0x0008C054
			public Core_ReferenceNotFound(Location location, params object[] args)
				: base("CORE001", GrammarValidation.Core, Severity.Error, "Referenced file not found: {0}.", location, args)
			{
			}

			// Token: 0x0400177B RID: 6011
			private const string id = "CORE001";

			// Token: 0x0400177C RID: 6012
			private const GrammarValidation category = GrammarValidation.Core;

			// Token: 0x0400177D RID: 6013
			private const Severity severity = Severity.Error;

			// Token: 0x0400177E RID: 6014
			private const string message = "Referenced file not found: {0}.";
		}

		// Token: 0x0200088F RID: 2191
		[DebuggerNonUserCode]
		internal sealed class Core_TypeLocationNotFound : Diagnostic
		{
			// Token: 0x06002FF8 RID: 12280 RVA: 0x0008DE6A File Offset: 0x0008C06A
			public Core_TypeLocationNotFound(Location location, params object[] args)
				: base("CORE002", GrammarValidation.Core, Severity.Error, "Type {0} is not found in any of the referenced assemblies. Check that it is public and is in scope of all imported namespaces.", location, args)
			{
			}

			// Token: 0x0400177F RID: 6015
			private const string id = "CORE002";

			// Token: 0x04001780 RID: 6016
			private const GrammarValidation category = GrammarValidation.Core;

			// Token: 0x04001781 RID: 6017
			private const Severity severity = Severity.Error;

			// Token: 0x04001782 RID: 6018
			private const string message = "Type {0} is not found in any of the referenced assemblies. Check that it is public and is in scope of all imported namespaces.";
		}

		// Token: 0x02000890 RID: 2192
		[DebuggerNonUserCode]
		internal sealed class Core_TypeIsNotStatic : Diagnostic
		{
			// Token: 0x06002FF9 RID: 12281 RVA: 0x0008DE80 File Offset: 0x0008C080
			public Core_TypeIsNotStatic(Location location, params object[] args)
				: base("CORE003", GrammarValidation.Core, Severity.Warning, "Type {0} should be a public static class.", location, args)
			{
			}

			// Token: 0x04001783 RID: 6019
			private const string id = "CORE003";

			// Token: 0x04001784 RID: 6020
			private const GrammarValidation category = GrammarValidation.Core;

			// Token: 0x04001785 RID: 6021
			private const Severity severity = Severity.Warning;

			// Token: 0x04001786 RID: 6022
			private const string message = "Type {0} should be a public static class.";
		}

		// Token: 0x02000891 RID: 2193
		[DebuggerNonUserCode]
		internal sealed class Core_MemberNotFound : Diagnostic
		{
			// Token: 0x06002FFA RID: 12282 RVA: 0x0008DE96 File Offset: 0x0008C096
			public Core_MemberNotFound(Location location, params object[] args)
				: base("CORE004", GrammarValidation.Core, Severity.Error, "Member {0} not found in the specified class {1}. Check that it is public and static.", location, args)
			{
			}

			// Token: 0x04001787 RID: 6023
			private const string id = "CORE004";

			// Token: 0x04001788 RID: 6024
			private const GrammarValidation category = GrammarValidation.Core;

			// Token: 0x04001789 RID: 6025
			private const Severity severity = Severity.Error;

			// Token: 0x0400178A RID: 6026
			private const string message = "Member {0} not found in the specified class {1}. Check that it is public and static.";
		}

		// Token: 0x02000892 RID: 2194
		[DebuggerNonUserCode]
		internal sealed class Core_UnknownReferenceType : Diagnostic
		{
			// Token: 0x06002FFB RID: 12283 RVA: 0x0008DEAC File Offset: 0x0008C0AC
			public Core_UnknownReferenceType(Location location, params object[] args)
				: base("CORE005", GrammarValidation.Core, Severity.Warning, "Cannot load a reference from the file '{0}'. Currently supported references are: .NET assemblies (.dll/.exe).", location, args)
			{
			}

			// Token: 0x0400178B RID: 6027
			private const string id = "CORE005";

			// Token: 0x0400178C RID: 6028
			private const GrammarValidation category = GrammarValidation.Core;

			// Token: 0x0400178D RID: 6029
			private const Severity severity = Severity.Warning;

			// Token: 0x0400178E RID: 6030
			private const string message = "Cannot load a reference from the file '{0}'. Currently supported references are: .NET assemblies (.dll/.exe).";
		}

		// Token: 0x02000893 RID: 2195
		[DebuggerNonUserCode]
		internal sealed class Core_UnknownExternalGrammar : Diagnostic
		{
			// Token: 0x06002FFC RID: 12284 RVA: 0x0008DEC2 File Offset: 0x0008C0C2
			public Core_UnknownExternalGrammar(Location location, params object[] args)
				: base("CORE006", GrammarValidation.Core, Severity.Error, "Unknown external language: '{0}'.", location, args)
			{
			}

			// Token: 0x0400178F RID: 6031
			private const string id = "CORE006";

			// Token: 0x04001790 RID: 6032
			private const GrammarValidation category = GrammarValidation.Core;

			// Token: 0x04001791 RID: 6033
			private const Severity severity = Severity.Error;

			// Token: 0x04001792 RID: 6034
			private const string message = "Unknown external language: '{0}'.";
		}

		// Token: 0x02000894 RID: 2196
		[DebuggerNonUserCode]
		internal sealed class Core_UnknownSymbol : Diagnostic
		{
			// Token: 0x06002FFD RID: 12285 RVA: 0x0008DED8 File Offset: 0x0008C0D8
			public Core_UnknownSymbol(Location location, params object[] args)
				: base("CORE007", GrammarValidation.Core, Severity.Error, "Symbol {0} was not found in the language {1}.", location, args)
			{
			}

			// Token: 0x04001793 RID: 6035
			private const string id = "CORE007";

			// Token: 0x04001794 RID: 6036
			private const GrammarValidation category = GrammarValidation.Core;

			// Token: 0x04001795 RID: 6037
			private const Severity severity = Severity.Error;

			// Token: 0x04001796 RID: 6038
			private const string message = "Symbol {0} was not found in the language {1}.";
		}

		// Token: 0x02000895 RID: 2197
		[DebuggerNonUserCode]
		internal sealed class Core_AssemblyNotFound : Diagnostic
		{
			// Token: 0x06002FFE RID: 12286 RVA: 0x0008DEEE File Offset: 0x0008C0EE
			public Core_AssemblyNotFound(Location location, params object[] args)
				: base("CORE008", GrammarValidation.Core, Severity.Error, "Could not resolve an assembly name '{0}'.", location, args)
			{
			}

			// Token: 0x04001797 RID: 6039
			private const string id = "CORE008";

			// Token: 0x04001798 RID: 6040
			private const GrammarValidation category = GrammarValidation.Core;

			// Token: 0x04001799 RID: 6041
			private const Severity severity = Severity.Error;

			// Token: 0x0400179A RID: 6042
			private const string message = "Could not resolve an assembly name '{0}'.";
		}

		// Token: 0x02000896 RID: 2198
		[DebuggerNonUserCode]
		internal sealed class Core_CompilationFailed : Diagnostic
		{
			// Token: 0x06002FFF RID: 12287 RVA: 0x0008DF04 File Offset: 0x0008C104
			public Core_CompilationFailed(Location location, params object[] args)
				: base("CORE009", GrammarValidation.Core, Severity.Error, "Could not compile grammar assembly.  Compiler messages: {0}", location, args)
			{
			}

			// Token: 0x0400179B RID: 6043
			private const string id = "CORE009";

			// Token: 0x0400179C RID: 6044
			private const GrammarValidation category = GrammarValidation.Core;

			// Token: 0x0400179D RID: 6045
			private const Severity severity = Severity.Error;

			// Token: 0x0400179E RID: 6046
			private const string message = "Could not compile grammar assembly.  Compiler messages: {0}";
		}

		// Token: 0x02000897 RID: 2199
		[DebuggerNonUserCode]
		internal sealed class Core_InvalidRuleName : Diagnostic
		{
			// Token: 0x06003000 RID: 12288 RVA: 0x0008DF1A File Offset: 0x0008C11A
			public Core_InvalidRuleName(Location location, params object[] args)
				: base("CORE010", GrammarValidation.Core, Severity.Warning, "Member {0} references invalid rule name '{1}'. This rule does not exist in the language {2}.", location, args)
			{
			}

			// Token: 0x0400179F RID: 6047
			private const string id = "CORE010";

			// Token: 0x040017A0 RID: 6048
			private const GrammarValidation category = GrammarValidation.Core;

			// Token: 0x040017A1 RID: 6049
			private const Severity severity = Severity.Warning;

			// Token: 0x040017A2 RID: 6050
			private const string message = "Member {0} references invalid rule name '{1}'. This rule does not exist in the language {2}.";
		}

		// Token: 0x02000898 RID: 2200
		[DebuggerNonUserCode]
		internal sealed class Core_UndeclaredExternalGrammar : Diagnostic
		{
			// Token: 0x06003001 RID: 12289 RVA: 0x0008DF30 File Offset: 0x0008C130
			public Core_UndeclaredExternalGrammar(Location location, params object[] args)
				: base("CORE011", GrammarValidation.Core, Severity.Error, "Undeclared external language: '{0}'. Use 'using grammar <name>=<class>.<property>' to refer to a previously compiled grammar", location, args)
			{
			}

			// Token: 0x040017A3 RID: 6051
			private const string id = "CORE011";

			// Token: 0x040017A4 RID: 6052
			private const GrammarValidation category = GrammarValidation.Core;

			// Token: 0x040017A5 RID: 6053
			private const Severity severity = Severity.Error;

			// Token: 0x040017A6 RID: 6054
			private const string message = "Undeclared external language: '{0}'. Use 'using grammar <name>=<class>.<property>' to refer to a previously compiled grammar";
		}

		// Token: 0x02000899 RID: 2201
		[DebuggerNonUserCode]
		internal sealed class Core_DuplicateExternalGrammar : Diagnostic
		{
			// Token: 0x06003002 RID: 12290 RVA: 0x0008DF46 File Offset: 0x0008C146
			public Core_DuplicateExternalGrammar(Location location, params object[] args)
				: base("CORE012", GrammarValidation.Core, Severity.Error, "Duplicate external language: '{0}'.", location, args)
			{
			}

			// Token: 0x040017A7 RID: 6055
			private const string id = "CORE012";

			// Token: 0x040017A8 RID: 6056
			private const GrammarValidation category = GrammarValidation.Core;

			// Token: 0x040017A9 RID: 6057
			private const Severity severity = Severity.Error;

			// Token: 0x040017AA RID: 6058
			private const string message = "Duplicate external language: '{0}'.";
		}

		// Token: 0x0200089A RID: 2202
		[DebuggerNonUserCode]
		internal sealed class Features_UnknownFeature : Diagnostic
		{
			// Token: 0x06003003 RID: 12291 RVA: 0x0008DF5C File Offset: 0x0008C15C
			public Features_UnknownFeature(Location location, params object[] args)
				: base("FEA001", GrammarValidation.Features, Severity.Error, "Feature '{0}' was not found in the language {1}.", location, args)
			{
			}

			// Token: 0x040017AB RID: 6059
			private const string id = "FEA001";

			// Token: 0x040017AC RID: 6060
			private const GrammarValidation category = GrammarValidation.Features;

			// Token: 0x040017AD RID: 6061
			private const Severity severity = Severity.Error;

			// Token: 0x040017AE RID: 6062
			private const string message = "Feature '{0}' was not found in the language {1}.";
		}

		// Token: 0x0200089B RID: 2203
		[DebuggerNonUserCode]
		internal sealed class Features_IncompatibleCalculatorReturnType : Diagnostic
		{
			// Token: 0x06003004 RID: 12292 RVA: 0x0008DF73 File Offset: 0x0008C173
			public Features_IncompatibleCalculatorReturnType(Location location, params object[] args)
				: base("FEA002", GrammarValidation.Features, Severity.Error, "In the feature calculator {0}, return type {1} cannot be converted to the required feature type {2}.", location, args)
			{
			}

			// Token: 0x040017AF RID: 6063
			private const string id = "FEA002";

			// Token: 0x040017B0 RID: 6064
			private const GrammarValidation category = GrammarValidation.Features;

			// Token: 0x040017B1 RID: 6065
			private const Severity severity = Severity.Error;

			// Token: 0x040017B2 RID: 6066
			private const string message = "In the feature calculator {0}, return type {1} cannot be converted to the required feature type {2}.";
		}

		// Token: 0x0200089C RID: 2204
		[DebuggerNonUserCode]
		internal sealed class Features_NoFeatureCalculator : Diagnostic
		{
			// Token: 0x06003005 RID: 12293 RVA: 0x0008DF8A File Offset: 0x0008C18A
			public Features_NoFeatureCalculator(Location location, params object[] args)
				: base("FEA003", GrammarValidation.Features, Severity.Error, "No '{0}' feature calculator was found for the rule {1}. Please define a public function in the class {2} and mark it with [FeatureCalculator] attribute.", location, args)
			{
			}

			// Token: 0x040017B3 RID: 6067
			private const string id = "FEA003";

			// Token: 0x040017B4 RID: 6068
			private const GrammarValidation category = GrammarValidation.Features;

			// Token: 0x040017B5 RID: 6069
			private const Severity severity = Severity.Error;

			// Token: 0x040017B6 RID: 6070
			private const string message = "No '{0}' feature calculator was found for the rule {1}. Please define a public function in the class {2} and mark it with [FeatureCalculator] attribute.";
		}

		// Token: 0x0200089D RID: 2205
		[DebuggerNonUserCode]
		internal sealed class Features_NoLearningInfoParameter : Diagnostic
		{
			// Token: 0x06003006 RID: 12294 RVA: 0x0008DFA1 File Offset: 0x0008C1A1
			public Features_NoLearningInfoParameter(Location location, params object[] args)
				: base("FEA004", GrammarValidation.Features, Severity.Error, "Expected a learning info parameter of type LearningInfo as the first parameter.", location, args)
			{
			}

			// Token: 0x040017B7 RID: 6071
			private const string id = "FEA004";

			// Token: 0x040017B8 RID: 6072
			private const GrammarValidation category = GrammarValidation.Features;

			// Token: 0x040017B9 RID: 6073
			private const Severity severity = Severity.Error;

			// Token: 0x040017BA RID: 6074
			private const string message = "Expected a learning info parameter of type LearningInfo as the first parameter.";
		}

		// Token: 0x0200089E RID: 2206
		[DebuggerNonUserCode]
		internal sealed class Features_ExpectedNonterminalCalculator : Diagnostic
		{
			// Token: 0x06003007 RID: 12295 RVA: 0x0008DFB8 File Offset: 0x0008C1B8
			public Features_ExpectedNonterminalCalculator(Location location, params object[] args)
				: base("FEA005", GrammarValidation.Features, Severity.Error, "In nonterminal feature calculator {0}, parameter '{1}' has type {2}, expected a type derived from ProgramNode.", location, args)
			{
			}

			// Token: 0x040017BB RID: 6075
			private const string id = "FEA005";

			// Token: 0x040017BC RID: 6076
			private const GrammarValidation category = GrammarValidation.Features;

			// Token: 0x040017BD RID: 6077
			private const Severity severity = Severity.Error;

			// Token: 0x040017BE RID: 6078
			private const string message = "In nonterminal feature calculator {0}, parameter '{1}' has type {2}, expected a type derived from ProgramNode.";
		}

		// Token: 0x0200089F RID: 2207
		[DebuggerNonUserCode]
		internal sealed class Features_ExpectedLiteralCalculator : Diagnostic
		{
			// Token: 0x06003008 RID: 12296 RVA: 0x0008DFCF File Offset: 0x0008C1CF
			public Features_ExpectedLiteralCalculator(Location location, params object[] args)
				: base("FEA006", GrammarValidation.Features, Severity.Error, "Literal feature calculator {0} has {1} parameters, expected {2}.", location, args)
			{
			}

			// Token: 0x040017BF RID: 6079
			private const string id = "FEA006";

			// Token: 0x040017C0 RID: 6080
			private const GrammarValidation category = GrammarValidation.Features;

			// Token: 0x040017C1 RID: 6081
			private const Severity severity = Severity.Error;

			// Token: 0x040017C2 RID: 6082
			private const string message = "Literal feature calculator {0} has {1} parameters, expected {2}.";
		}

		// Token: 0x020008A0 RID: 2208
		[DebuggerNonUserCode]
		internal sealed class Features_ExpectedRecursiveCalculator : Diagnostic
		{
			// Token: 0x06003009 RID: 12297 RVA: 0x0008DFE6 File Offset: 0x0008C1E6
			public Features_ExpectedRecursiveCalculator(Location location, params object[] args)
				: base("FEA007", GrammarValidation.Features, Severity.Error, "In feature '{0}' calculator {1}, parameter '{2}' has type {3}, expected {4}.", location, args)
			{
			}

			// Token: 0x040017C3 RID: 6083
			private const string id = "FEA007";

			// Token: 0x040017C4 RID: 6084
			private const GrammarValidation category = GrammarValidation.Features;

			// Token: 0x040017C5 RID: 6085
			private const Severity severity = Severity.Error;

			// Token: 0x040017C6 RID: 6086
			private const string message = "In feature '{0}' calculator {1}, parameter '{2}' has type {3}, expected {4}.";
		}

		// Token: 0x020008A1 RID: 2209
		[DebuggerNonUserCode]
		internal sealed class Features_AmbiguousFeatureCalculator : Diagnostic
		{
			// Token: 0x0600300A RID: 12298 RVA: 0x0008DFFD File Offset: 0x0008C1FD
			public Features_AmbiguousFeatureCalculator(Location location, params object[] args)
				: base("FEA008", GrammarValidation.Features, Severity.Error, "Ambiguous '{0}' feature calculator for the rule {1}: cannot choose between {2} and {3}.", location, args)
			{
			}

			// Token: 0x040017C7 RID: 6087
			private const string id = "FEA008";

			// Token: 0x040017C8 RID: 6088
			private const GrammarValidation category = GrammarValidation.Features;

			// Token: 0x040017C9 RID: 6089
			private const Severity severity = Severity.Error;

			// Token: 0x040017CA RID: 6090
			private const string message = "Ambiguous '{0}' feature calculator for the rule {1}: cannot choose between {2} and {3}.";
		}

		// Token: 0x020008A2 RID: 2210
		[DebuggerNonUserCode]
		internal sealed class Features_InvalidRuleName : Diagnostic
		{
			// Token: 0x0600300B RID: 12299 RVA: 0x0008E014 File Offset: 0x0008C214
			public Features_InvalidRuleName(Location location, params object[] args)
				: base("FEA009", GrammarValidation.Features, Severity.Error, "In feature '{0}' calculator {1}, the rule name '{2}' is invalid. Rule names must be valid C# identifiers (may contain only alphanumeric symbols and underscores).", location, args)
			{
			}

			// Token: 0x040017CB RID: 6091
			private const string id = "FEA009";

			// Token: 0x040017CC RID: 6092
			private const GrammarValidation category = GrammarValidation.Features;

			// Token: 0x040017CD RID: 6093
			private const Severity severity = Severity.Error;

			// Token: 0x040017CE RID: 6094
			private const string message = "In feature '{0}' calculator {1}, the rule name '{2}' is invalid. Rule names must be valid C# identifiers (may contain only alphanumeric symbols and underscores).";
		}

		// Token: 0x020008A3 RID: 2211
		[DebuggerNonUserCode]
		internal sealed class Features_VarDefaultNotGiven : Diagnostic
		{
			// Token: 0x0600300C RID: 12300 RVA: 0x0008E02B File Offset: 0x0008C22B
			public Features_VarDefaultNotGiven(Location location, params object[] args)
				: base("FEA010", GrammarValidation.Features, Severity.Warning, "Implementation {0} of a complete feature '{1}' does not override the default implementation of {2}, which calculates the value of this feature on variable nodes. {3} will be used instead in order to make the feature complete. If this is not intentional, either remove the @complete annotation or override the method with your implementation", location, args)
			{
			}

			// Token: 0x040017CF RID: 6095
			private const string id = "FEA010";

			// Token: 0x040017D0 RID: 6096
			private const GrammarValidation category = GrammarValidation.Features;

			// Token: 0x040017D1 RID: 6097
			private const Severity severity = Severity.Warning;

			// Token: 0x040017D2 RID: 6098
			private const string message = "Implementation {0} of a complete feature '{1}' does not override the default implementation of {2}, which calculates the value of this feature on variable nodes. {3} will be used instead in order to make the feature complete. If this is not intentional, either remove the @complete annotation or override the method with your implementation";
		}

		// Token: 0x020008A4 RID: 2212
		[DebuggerNonUserCode]
		internal sealed class Features_UnknownRuleName : Diagnostic
		{
			// Token: 0x0600300D RID: 12301 RVA: 0x0008E042 File Offset: 0x0008C242
			public Features_UnknownRuleName(Location location, params object[] args)
				: base("FEA020", GrammarValidation.Features, Severity.Warning, "Member {0} references invalid rule name '{1}'. This rule does not exist in the language {2}.", location, args)
			{
			}

			// Token: 0x040017D3 RID: 6099
			private const string id = "FEA020";

			// Token: 0x040017D4 RID: 6100
			private const GrammarValidation category = GrammarValidation.Features;

			// Token: 0x040017D5 RID: 6101
			private const Severity severity = Severity.Warning;

			// Token: 0x040017D6 RID: 6102
			private const string message = "Member {0} references invalid rule name '{1}'. This rule does not exist in the language {2}.";
		}

		// Token: 0x020008A5 RID: 2213
		[DebuggerNonUserCode]
		internal sealed class Features_IncompatibleCalculatorParameters : Diagnostic
		{
			// Token: 0x0600300E RID: 12302 RVA: 0x0008E059 File Offset: 0x0008C259
			public Features_IncompatibleCalculatorParameters(Location location, params object[] args)
				: base("FEA021", GrammarValidation.Features, Severity.Error, "The feature calculator {0} has {2} parameters but must have {3} parameters to match its rule {1}.", location, args)
			{
			}

			// Token: 0x040017D7 RID: 6103
			private const string id = "FEA021";

			// Token: 0x040017D8 RID: 6104
			private const GrammarValidation category = GrammarValidation.Features;

			// Token: 0x040017D9 RID: 6105
			private const Severity severity = Severity.Error;

			// Token: 0x040017DA RID: 6106
			private const string message = "The feature calculator {0} has {2} parameters but must have {3} parameters to match its rule {1}.";
		}

		// Token: 0x020008A6 RID: 2214
		[DebuggerNonUserCode]
		internal sealed class Syntax_SymbolCircularDependency : Diagnostic
		{
			// Token: 0x0600300F RID: 12303 RVA: 0x0008E070 File Offset: 0x0008C270
			public Syntax_SymbolCircularDependency(Location location, params object[] args)
				: base("SYN001", GrammarValidation.Syntax, Severity.Error, "Circular symbol dependency detected: {0}.", location, args)
			{
			}

			// Token: 0x040017DB RID: 6107
			private const string id = "SYN001";

			// Token: 0x040017DC RID: 6108
			private const GrammarValidation category = GrammarValidation.Syntax;

			// Token: 0x040017DD RID: 6109
			private const Severity severity = Severity.Error;

			// Token: 0x040017DE RID: 6110
			private const string message = "Circular symbol dependency detected: {0}.";
		}

		// Token: 0x020008A7 RID: 2215
		[DebuggerNonUserCode]
		internal sealed class Syntax_NoStartSymbols : Diagnostic
		{
			// Token: 0x06003010 RID: 12304 RVA: 0x0008E086 File Offset: 0x0008C286
			public Syntax_NoStartSymbols(Location location, params object[] args)
				: base("SYN002", GrammarValidation.Syntax, Severity.Error, "No start symbols found. Please mark one of the nonterminals of the language as @start.", location, args)
			{
			}

			// Token: 0x040017DF RID: 6111
			private const string id = "SYN002";

			// Token: 0x040017E0 RID: 6112
			private const GrammarValidation category = GrammarValidation.Syntax;

			// Token: 0x040017E1 RID: 6113
			private const Severity severity = Severity.Error;

			// Token: 0x040017E2 RID: 6114
			private const string message = "No start symbols found. Please mark one of the nonterminals of the language as @start.";
		}

		// Token: 0x020008A8 RID: 2216
		[DebuggerNonUserCode]
		internal sealed class Syntax_MoreThanOneStart : Diagnostic
		{
			// Token: 0x06003011 RID: 12305 RVA: 0x0008E09C File Offset: 0x0008C29C
			public Syntax_MoreThanOneStart(Location location, params object[] args)
				: base("SYN003", GrammarValidation.Syntax, Severity.Warning, "More then one start symbol detected: {0}.", location, args)
			{
			}

			// Token: 0x040017E3 RID: 6115
			private const string id = "SYN003";

			// Token: 0x040017E4 RID: 6116
			private const GrammarValidation category = GrammarValidation.Syntax;

			// Token: 0x040017E5 RID: 6117
			private const Severity severity = Severity.Warning;

			// Token: 0x040017E6 RID: 6118
			private const string message = "More then one start symbol detected: {0}.";
		}

		// Token: 0x020008A9 RID: 2217
		[DebuggerNonUserCode]
		internal sealed class Syntax_NoInputSymbols : Diagnostic
		{
			// Token: 0x06003012 RID: 12306 RVA: 0x0008E0B2 File Offset: 0x0008C2B2
			public Syntax_NoInputSymbols(Location location, params object[] args)
				: base("SYN004", GrammarValidation.Syntax, Severity.Warning, "No input symbols found. Please mark one of the terminals of the language as @input.", location, args)
			{
			}

			// Token: 0x040017E7 RID: 6119
			private const string id = "SYN004";

			// Token: 0x040017E8 RID: 6120
			private const GrammarValidation category = GrammarValidation.Syntax;

			// Token: 0x040017E9 RID: 6121
			private const Severity severity = Severity.Warning;

			// Token: 0x040017EA RID: 6122
			private const string message = "No input symbols found. Please mark one of the terminals of the language as @input.";
		}

		// Token: 0x020008AA RID: 2218
		[DebuggerNonUserCode]
		internal sealed class Syntax_MoreThanOneInput : Diagnostic
		{
			// Token: 0x06003013 RID: 12307 RVA: 0x0008E0C8 File Offset: 0x0008C2C8
			public Syntax_MoreThanOneInput(Location location, params object[] args)
				: base("SYN005", GrammarValidation.Syntax, Severity.Error, "More then one input symbol detected: {0}.", location, args)
			{
			}

			// Token: 0x040017EB RID: 6123
			private const string id = "SYN005";

			// Token: 0x040017EC RID: 6124
			private const GrammarValidation category = GrammarValidation.Syntax;

			// Token: 0x040017ED RID: 6125
			private const Severity severity = Severity.Error;

			// Token: 0x040017EE RID: 6126
			private const string message = "More then one input symbol detected: {0}.";
		}

		// Token: 0x020008AB RID: 2219
		[DebuggerNonUserCode]
		internal sealed class Syntax_ConceptParametersShouldBeArguments : Diagnostic
		{
			// Token: 0x06003014 RID: 12308 RVA: 0x0008E0DE File Offset: 0x0008C2DE
			public Syntax_ConceptParametersShouldBeArguments(Location location, params object[] args)
				: base("SYN006", GrammarValidation.Syntax, Severity.Error, "In concept rule {0}, the set of formal parameters {1} on the left-hand side should be equal to the set of the unbound symbols {2} on the right-hand side.", location, args)
			{
			}

			// Token: 0x040017EF RID: 6127
			private const string id = "SYN006";

			// Token: 0x040017F0 RID: 6128
			private const GrammarValidation category = GrammarValidation.Syntax;

			// Token: 0x040017F1 RID: 6129
			private const Severity severity = Severity.Error;

			// Token: 0x040017F2 RID: 6130
			private const string message = "In concept rule {0}, the set of formal parameters {1} on the left-hand side should be equal to the set of the unbound symbols {2} on the right-hand side.";
		}

		// Token: 0x020008AC RID: 2220
		[DebuggerNonUserCode]
		internal sealed class Syntax_IncompatibleSymbolTypes : Diagnostic
		{
			// Token: 0x06003015 RID: 12309 RVA: 0x0008E0F4 File Offset: 0x0008C2F4
			public Syntax_IncompatibleSymbolTypes(Location location, params object[] args)
				: base("SYN007", GrammarValidation.Syntax, Severity.Error, "Symbol {0} has type {1}, but is initialized with a symbol {2}, which has type {3}", location, args)
			{
			}

			// Token: 0x040017F3 RID: 6131
			private const string id = "SYN007";

			// Token: 0x040017F4 RID: 6132
			private const GrammarValidation category = GrammarValidation.Syntax;

			// Token: 0x040017F5 RID: 6133
			private const Severity severity = Severity.Error;

			// Token: 0x040017F6 RID: 6134
			private const string message = "Symbol {0} has type {1}, but is initialized with a symbol {2}, which has type {3}";
		}

		// Token: 0x020008AD RID: 2221
		[DebuggerNonUserCode]
		internal sealed class Syntax_NoLanguageName : Diagnostic
		{
			// Token: 0x06003016 RID: 12310 RVA: 0x0008E10A File Offset: 0x0008C30A
			public Syntax_NoLanguageName(Location location, params object[] args)
				: base("SYN008", GrammarValidation.Syntax, Severity.Error, "No language name found.", location, args)
			{
			}

			// Token: 0x040017F7 RID: 6135
			private const string id = "SYN008";

			// Token: 0x040017F8 RID: 6136
			private const GrammarValidation category = GrammarValidation.Syntax;

			// Token: 0x040017F9 RID: 6137
			private const Severity severity = Severity.Error;

			// Token: 0x040017FA RID: 6138
			private const string message = "No language name found.";
		}

		// Token: 0x020008AE RID: 2222
		[DebuggerNonUserCode]
		internal sealed class Syntax_DuplicateId : Diagnostic
		{
			// Token: 0x06003017 RID: 12311 RVA: 0x0008E120 File Offset: 0x0008C320
			public Syntax_DuplicateId(Location location, params object[] args)
				: base("SYN009", GrammarValidation.Syntax, Severity.Error, "Id {0} was already declared.", location, args)
			{
			}

			// Token: 0x040017FB RID: 6139
			private const string id = "SYN009";

			// Token: 0x040017FC RID: 6140
			private const GrammarValidation category = GrammarValidation.Syntax;

			// Token: 0x040017FD RID: 6141
			private const Severity severity = Severity.Error;

			// Token: 0x040017FE RID: 6142
			private const string message = "Id {0} was already declared.";
		}

		// Token: 0x020008AF RID: 2223
		[DebuggerNonUserCode]
		internal sealed class Syntax_InvalidSymbolName : Diagnostic
		{
			// Token: 0x06003018 RID: 12312 RVA: 0x0008E136 File Offset: 0x0008C336
			public Syntax_InvalidSymbolName(Location location, params object[] args)
				: base("SYN010", GrammarValidation.Syntax, Severity.Error, "Invalid symbol identifier. Please ensure the symbol names match [_a-zA-Z][_a-zA-Z0-9]* and do not clash with keywords", location, args)
			{
			}

			// Token: 0x040017FF RID: 6143
			private const string id = "SYN010";

			// Token: 0x04001800 RID: 6144
			private const GrammarValidation category = GrammarValidation.Syntax;

			// Token: 0x04001801 RID: 6145
			private const Severity severity = Severity.Error;

			// Token: 0x04001802 RID: 6146
			private const string message = "Invalid symbol identifier. Please ensure the symbol names match [_a-zA-Z][_a-zA-Z0-9]* and do not clash with keywords";
		}

		// Token: 0x020008B0 RID: 2224
		[DebuggerNonUserCode]
		internal sealed class Semantics_NoSemantics : Diagnostic
		{
			// Token: 0x06003019 RID: 12313 RVA: 0x0008E14C File Offset: 0x0008C34C
			public Semantics_NoSemantics(Location location, params object[] args)
				: base("SEM001", GrammarValidation.Semantics, Severity.Error, "No semantics found for the rule {0}. Expected a function with signature 'public static {1} {0}({2})' in any of the semantics locations.", location, args)
			{
			}

			// Token: 0x04001803 RID: 6147
			private const string id = "SEM001";

			// Token: 0x04001804 RID: 6148
			private const GrammarValidation category = GrammarValidation.Semantics;

			// Token: 0x04001805 RID: 6149
			private const Severity severity = Severity.Error;

			// Token: 0x04001806 RID: 6150
			private const string message = "No semantics found for the rule {0}. Expected a function with signature 'public static {1} {0}({2})' in any of the semantics locations.";
		}

		// Token: 0x020008B1 RID: 2225
		[DebuggerNonUserCode]
		internal sealed class Semantics_AmbiguousSemantics : Diagnostic
		{
			// Token: 0x0600301A RID: 12314 RVA: 0x0008E162 File Offset: 0x0008C362
			public Semantics_AmbiguousSemantics(Location location, params object[] args)
				: base("SEM002", GrammarValidation.Semantics, Severity.Error, "Ambiguous semantics for the rule {0}: cannot choose between {1} and {2}.", location, args)
			{
			}

			// Token: 0x04001807 RID: 6151
			private const string id = "SEM002";

			// Token: 0x04001808 RID: 6152
			private const GrammarValidation category = GrammarValidation.Semantics;

			// Token: 0x04001809 RID: 6153
			private const Severity severity = Severity.Error;

			// Token: 0x0400180A RID: 6154
			private const string message = "Ambiguous semantics for the rule {0}: cannot choose between {1} and {2}.";
		}

		// Token: 0x020008B2 RID: 2226
		[DebuggerNonUserCode]
		internal sealed class Semantics_IncompatibleSemanticsReturnType : Diagnostic
		{
			// Token: 0x0600301B RID: 12315 RVA: 0x0008E178 File Offset: 0x0008C378
			public Semantics_IncompatibleSemanticsReturnType(Location location, params object[] args)
				: base("SEM003", GrammarValidation.Semantics, Severity.Error, "Semantics function {0} returns {1}, expected {2}.", location, args)
			{
			}

			// Token: 0x0400180B RID: 6155
			private const string id = "SEM003";

			// Token: 0x0400180C RID: 6156
			private const GrammarValidation category = GrammarValidation.Semantics;

			// Token: 0x0400180D RID: 6157
			private const Severity severity = Severity.Error;

			// Token: 0x0400180E RID: 6158
			private const string message = "Semantics function {0} returns {1}, expected {2}.";
		}

		// Token: 0x020008B3 RID: 2227
		[DebuggerNonUserCode]
		internal sealed class Semantics_IncompatibleConceptArgumentType : Diagnostic
		{
			// Token: 0x0600301C RID: 12316 RVA: 0x0008E18E File Offset: 0x0008C38E
			public Semantics_IncompatibleConceptArgumentType(Location location, params object[] args)
				: base("SEM004", GrammarValidation.Semantics, Severity.Error, "Invalid type {0} of symbol {1}: the concept '{2}' expects an argument of type {3} here.", location, args)
			{
			}

			// Token: 0x0400180F RID: 6159
			private const string id = "SEM004";

			// Token: 0x04001810 RID: 6160
			private const GrammarValidation category = GrammarValidation.Semantics;

			// Token: 0x04001811 RID: 6161
			private const Severity severity = Severity.Error;

			// Token: 0x04001812 RID: 6162
			private const string message = "Invalid type {0} of symbol {1}: the concept '{2}' expects an argument of type {3} here.";
		}

		// Token: 0x020008B4 RID: 2228
		[DebuggerNonUserCode]
		internal sealed class Semantics_IncompatibleConceptResultType : Diagnostic
		{
			// Token: 0x0600301D RID: 12317 RVA: 0x0008E1A4 File Offset: 0x0008C3A4
			public Semantics_IncompatibleConceptResultType(Location location, params object[] args)
				: base("SEM005", GrammarValidation.Semantics, Severity.Error, "Invalid type {0} of symbol {1}: the concept '{2}' returns a type {3} here.", location, args)
			{
			}

			// Token: 0x04001813 RID: 6163
			private const string id = "SEM005";

			// Token: 0x04001814 RID: 6164
			private const GrammarValidation category = GrammarValidation.Semantics;

			// Token: 0x04001815 RID: 6165
			private const Severity severity = Severity.Error;

			// Token: 0x04001816 RID: 6166
			private const string message = "Invalid type {0} of symbol {1}: the concept '{2}' returns a type {3} here.";
		}

		// Token: 0x020008B5 RID: 2229
		[DebuggerNonUserCode]
		internal sealed class DeductiveLearning_PlanNotFound : Diagnostic
		{
			// Token: 0x0600301E RID: 12318 RVA: 0x0008E1BA File Offset: 0x0008C3BA
			public DeductiveLearning_PlanNotFound(Location location, params object[] args)
				: base("TDL001", GrammarValidation.DeductiveLearning, Severity.Warning, "Plan class {0} is not found in any of the referenced assemblies. Check that it is a public static class.", location, args)
			{
			}

			// Token: 0x04001817 RID: 6167
			private const string id = "TDL001";

			// Token: 0x04001818 RID: 6168
			private const GrammarValidation category = GrammarValidation.DeductiveLearning;

			// Token: 0x04001819 RID: 6169
			private const Severity severity = Severity.Warning;

			// Token: 0x0400181A RID: 6170
			private const string message = "Plan class {0} is not found in any of the referenced assemblies. Check that it is a public static class.";
		}

		// Token: 0x020008B6 RID: 2230
		[DebuggerNonUserCode]
		internal sealed class DeductiveLearning_IgnoredTerminalWitnesses : Diagnostic
		{
			// Token: 0x0600301F RID: 12319 RVA: 0x0008E1D0 File Offset: 0x0008C3D0
			public DeductiveLearning_IgnoredTerminalWitnesses(Location location, params object[] args)
				: base("TDL002", GrammarValidation.DeductiveLearning, Severity.Warning, "Illegal witness function specification for the terminal rule {0}, ignored.", location, args)
			{
			}

			// Token: 0x0400181B RID: 6171
			private const string id = "TDL002";

			// Token: 0x0400181C RID: 6172
			private const GrammarValidation category = GrammarValidation.DeductiveLearning;

			// Token: 0x0400181D RID: 6173
			private const Severity severity = Severity.Warning;

			// Token: 0x0400181E RID: 6174
			private const string message = "Illegal witness function specification for the terminal rule {0}, ignored.";
		}

		// Token: 0x020008B7 RID: 2231
		[DebuggerNonUserCode]
		internal sealed class DeductiveLearning_IncompatibleWitnessRuleType : Diagnostic
		{
			// Token: 0x06003020 RID: 12320 RVA: 0x0008E1E6 File Offset: 0x0008C3E6
			public DeductiveLearning_IncompatibleWitnessRuleType(Location location, params object[] args)
				: base("TDL003", GrammarValidation.DeductiveLearning, Severity.Error, "Witness function {0} expects rule type {1} as its first parameter, but the rule {2} has type {3}, which cannot be converted to {1}.", location, args)
			{
			}

			// Token: 0x0400181F RID: 6175
			private const string id = "TDL003";

			// Token: 0x04001820 RID: 6176
			private const GrammarValidation category = GrammarValidation.DeductiveLearning;

			// Token: 0x04001821 RID: 6177
			private const Severity severity = Severity.Error;

			// Token: 0x04001822 RID: 6178
			private const string message = "Witness function {0} expects rule type {1} as its first parameter, but the rule {2} has type {3}, which cannot be converted to {1}.";
		}

		// Token: 0x020008B8 RID: 2232
		[DebuggerNonUserCode]
		internal sealed class DeductiveLearning_IncompatibleWitnessSignature : Diagnostic
		{
			// Token: 0x06003021 RID: 12321 RVA: 0x0008E1FC File Offset: 0x0008C3FC
			public DeductiveLearning_IncompatibleWitnessSignature(Location location, params object[] args)
				: base("TDL004", GrammarValidation.DeductiveLearning, Severity.Error, "Witness function {0} has a signature that does not derive from (GrammarRule rule, Spec outerSpec, [, Spec prereqs...]) -> Spec.", location, args)
			{
			}

			// Token: 0x04001823 RID: 6179
			private const string id = "TDL004";

			// Token: 0x04001824 RID: 6180
			private const GrammarValidation category = GrammarValidation.DeductiveLearning;

			// Token: 0x04001825 RID: 6181
			private const Severity severity = Severity.Error;

			// Token: 0x04001826 RID: 6182
			private const string message = "Witness function {0} has a signature that does not derive from (GrammarRule rule, Spec outerSpec, [, Spec prereqs...]) -> Spec.";
		}

		// Token: 0x020008B9 RID: 2233
		[DebuggerNonUserCode]
		internal sealed class DeductiveLearning_IncompatibleWitnessPrereqTypes : Diagnostic
		{
			// Token: 0x06003022 RID: 12322 RVA: 0x0008E212 File Offset: 0x0008C412
			public DeductiveLearning_IncompatibleWitnessPrereqTypes(Location location, params object[] args)
				: base("TDL005", GrammarValidation.DeductiveLearning, Severity.Error, "Witness function {0} has invalid prerequisite type(s). They must occur after the required GrammarRule and Spec and be in a single parameter of type Spec[] or a series of parameters of types deriving from Spec.", location, args)
			{
			}

			// Token: 0x04001827 RID: 6183
			private const string id = "TDL005";

			// Token: 0x04001828 RID: 6184
			private const GrammarValidation category = GrammarValidation.DeductiveLearning;

			// Token: 0x04001829 RID: 6185
			private const Severity severity = Severity.Error;

			// Token: 0x0400182A RID: 6186
			private const string message = "Witness function {0} has invalid prerequisite type(s). They must occur after the required GrammarRule and Spec and be in a single parameter of type Spec[] or a series of parameters of types deriving from Spec.";
		}

		// Token: 0x020008BA RID: 2234
		[DebuggerNonUserCode]
		internal sealed class DeductiveLearning_IncompatibleWitnessParameter : Diagnostic
		{
			// Token: 0x06003023 RID: 12323 RVA: 0x0008E228 File Offset: 0x0008C428
			public DeductiveLearning_IncompatibleWitnessParameter(Location location, params object[] args)
				: base("TDL006", GrammarValidation.DeductiveLearning, Severity.Error, "Witness function {0} has a parameter specification that is out of range of valid parameters for the body of the rule {1}.", location, args)
			{
			}

			// Token: 0x0400182B RID: 6187
			private const string id = "TDL006";

			// Token: 0x0400182C RID: 6188
			private const GrammarValidation category = GrammarValidation.DeductiveLearning;

			// Token: 0x0400182D RID: 6189
			private const Severity severity = Severity.Error;

			// Token: 0x0400182E RID: 6190
			private const string message = "Witness function {0} has a parameter specification that is out of range of valid parameters for the body of the rule {1}.";
		}
	}
}
