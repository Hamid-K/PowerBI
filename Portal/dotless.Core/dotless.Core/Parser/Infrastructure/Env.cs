using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using dotless.Core.Loggers;
using dotless.Core.Parser.Functions;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Plugins;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Infrastructure
{
	// Token: 0x02000051 RID: 81
	public class Env
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		public Stack<Ruleset> Frames { get; protected set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000EDE1 File Offset: 0x0000CFE1
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000EDE9 File Offset: 0x0000CFE9
		public bool Compress { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000EDF2 File Offset: 0x0000CFF2
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000EDFA File Offset: 0x0000CFFA
		public bool Debug { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000EE03 File Offset: 0x0000D003
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000EE0B File Offset: 0x0000D00B
		public Node Rule { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000EE14 File Offset: 0x0000D014
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public ILogger Logger { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000EE25 File Offset: 0x0000D025
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000EE2D File Offset: 0x0000D02D
		public Output Output { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000EE36 File Offset: 0x0000D036
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000EE3E File Offset: 0x0000D03E
		public Stack<Media> MediaPath { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000EE47 File Offset: 0x0000D047
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000EE4F File Offset: 0x0000D04F
		public List<Media> MediaBlocks { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000EE58 File Offset: 0x0000D058
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000EE60 File Offset: 0x0000D060
		[Obsolete("The Variable Redefines feature has been removed to align with less.js")]
		public bool DisableVariableRedefines { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000EE69 File Offset: 0x0000D069
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000EE71 File Offset: 0x0000D071
		[Obsolete("The Color Compression feature has been removed to align with less.js")]
		public bool DisableColorCompression { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000EE7A File Offset: 0x0000D07A
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000EE82 File Offset: 0x0000D082
		public bool KeepFirstSpecialComment { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000EE8B File Offset: 0x0000D08B
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000EE93 File Offset: 0x0000D093
		public bool IsFirstSpecialCommentOutput { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000EE9C File Offset: 0x0000D09C
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
		public Parser Parser { get; set; }

		// Token: 0x06000377 RID: 887 RVA: 0x0000EEAD File Offset: 0x0000D0AD
		public Env()
			: this(new Parser())
		{
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000EEBA File Offset: 0x0000D0BA
		public Env(Parser parser)
			: this(parser, null, null)
		{
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000EEC5 File Offset: 0x0000D0C5
		protected Env(Parser parser, Stack<Ruleset> frames, Dictionary<string, Type> functions)
			: this(frames, functions)
		{
			this.Parser = parser;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		protected Env(Stack<Ruleset> frames, Dictionary<string, Type> functions)
		{
			this.Frames = frames ?? new Stack<Ruleset>();
			this.Output = new Output(this);
			this.MediaPath = new Stack<Media>();
			this.MediaBlocks = new List<Media>();
			this.Logger = new NullLogger(LogLevel.Info);
			this._plugins = new List<IPlugin>();
			this._functionTypes = functions ?? new Dictionary<string, Type>();
			this._extensions = new List<Extender>();
			this.ExtendMediaScope = new Stack<Media>();
			if (this._functionTypes.Count == 0)
			{
				this.AddCoreFunctions();
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000EF6D File Offset: 0x0000D16D
		[Obsolete("Argument is ignored as of version 1.4.3.0. Use the parameterless overload of CreateChildEnv instead.", false)]
		public virtual Env CreateChildEnv(Stack<Ruleset> ruleset)
		{
			return this.CreateChildEnv();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000EF78 File Offset: 0x0000D178
		public virtual Env CreateChildEnv()
		{
			return new Env(null, this._functionTypes)
			{
				Parser = this.Parser,
				Parent = this,
				Debug = this.Debug,
				Compress = this.Compress,
				DisableVariableRedefines = this.DisableVariableRedefines
			};
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		private Env Parent { get; set; }

		// Token: 0x0600037F RID: 895 RVA: 0x0000EFD9 File Offset: 0x0000D1D9
		public virtual Env CreateVariableEvaluationEnv(string variableName)
		{
			Env env = this.CreateChildEnv();
			env.EvaluatingVariable = variableName;
			return env;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
		private string EvaluatingVariable { get; set; }

		// Token: 0x06000382 RID: 898 RVA: 0x0000EFF9 File Offset: 0x0000D1F9
		public bool IsEvaluatingVariable(string variableName)
		{
			return string.Equals(variableName, this.EvaluatingVariable, StringComparison.InvariantCulture) || (this.Parent != null && this.Parent.IsEvaluatingVariable(variableName));
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000F022 File Offset: 0x0000D222
		public virtual Env CreateChildEnvWithClosure(Closure closure)
		{
			Env env = this.CreateChildEnv();
			env.Rule = this.Rule;
			env.ClosureEnvironment = new Env(new Stack<Ruleset>(closure.Context), this._functionTypes);
			return env;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000F052 File Offset: 0x0000D252
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000F05A File Offset: 0x0000D25A
		private Env ClosureEnvironment { get; set; }

		// Token: 0x06000386 RID: 902 RVA: 0x0000F064 File Offset: 0x0000D264
		public void AddPlugin(IPlugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			this._plugins.Add(plugin);
			IFunctionPlugin functionPlugin = plugin as IFunctionPlugin;
			if (functionPlugin != null)
			{
				foreach (KeyValuePair<string, Type> keyValuePair in functionPlugin.GetFunctions())
				{
					string text = keyValuePair.Key.ToLowerInvariant();
					if (this._functionTypes.ContainsKey(text))
					{
						throw new InvalidOperationException(string.Format("Function '{0}' already exists in environment but is added by plugin {1}", text, plugin.GetName()));
					}
					this.AddFunction(text, keyValuePair.Value);
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000F114 File Offset: 0x0000D314
		public IEnumerable<IVisitorPlugin> VisitorPlugins
		{
			get
			{
				return this._plugins.OfType<IVisitorPlugin>();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000F121 File Offset: 0x0000D321
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000F129 File Offset: 0x0000D329
		public Stack<Media> ExtendMediaScope { get; set; }

		// Token: 0x0600038A RID: 906 RVA: 0x0000F132 File Offset: 0x0000D332
		public bool IsCommentSilent(bool isValidCss, bool isCssHack, bool isSpecialComment)
		{
			if (!isValidCss)
			{
				return true;
			}
			if (isCssHack)
			{
				return false;
			}
			if (this.Compress && this.KeepFirstSpecialComment && !this.IsFirstSpecialCommentOutput && isSpecialComment)
			{
				this.IsFirstSpecialCommentOutput = true;
				return false;
			}
			return this.Compress;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000F16D File Offset: 0x0000D36D
		public Rule FindVariable(string name)
		{
			return this.FindVariable(name, this.Rule);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000F17C File Offset: 0x0000D37C
		public Rule FindVariable(string name, Node rule)
		{
			foreach (Ruleset ruleset in this.Frames)
			{
				Rule rule2 = ruleset.Variable(name, null);
				if (rule2)
				{
					return rule2;
				}
			}
			Rule rule3 = null;
			if (this.Parent != null)
			{
				rule3 = this.Parent.FindVariable(name, rule);
			}
			if (rule3 != null)
			{
				return rule3;
			}
			if (this.ClosureEnvironment != null)
			{
				return this.ClosureEnvironment.FindVariable(name, rule);
			}
			return null;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000F218 File Offset: 0x0000D418
		[Obsolete("This method will be removed in a future release.", false)]
		public IEnumerable<Closure> FindRulesets<TRuleset>(Selector selector) where TRuleset : Ruleset
		{
			return from c in this.FindRulesets(selector)
				where c.Ruleset is TRuleset
				select c;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000F248 File Offset: 0x0000D448
		public IEnumerable<Closure> FindRulesets(Selector selector)
		{
			List<Closure> list = this.Frames.Reverse<Ruleset>().SelectMany((Ruleset frame) => frame.Find<Ruleset>(this, selector, null)).Where(delegate(Closure matchedClosure)
			{
				if (!this.Frames.Any((Ruleset frame) => frame.IsEqualOrClonedFrom(matchedClosure.Ruleset)))
				{
					return true;
				}
				MixinDefinition mixinDefinition = matchedClosure.Ruleset as MixinDefinition;
				return mixinDefinition != null && mixinDefinition.Condition != null;
			})
				.ToList<Closure>();
			if (list.Any<Closure>())
			{
				return list;
			}
			if (this.Parent != null)
			{
				IEnumerable<Closure> enumerable = this.Parent.FindRulesets(selector);
				if (enumerable != null)
				{
					return enumerable;
				}
			}
			if (this.ClosureEnvironment != null)
			{
				return this.ClosureEnvironment.FindRulesets(selector);
			}
			return null;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		public void AddFunction(string name, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this._functionTypes[name] = type;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000F314 File Offset: 0x0000D514
		public void AddFunctionsFromAssembly(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			Dictionary<string, Type> functionsFromAssembly = Env.GetFunctionsFromAssembly(assembly);
			this.AddFunctionsToRegistry(functionsFromAssembly);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000F344 File Offset: 0x0000D544
		private void AddFunctionsToRegistry(IEnumerable<KeyValuePair<string, Type>> functions)
		{
			foreach (KeyValuePair<string, Type> keyValuePair in functions)
			{
				this.AddFunction(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000F39C File Offset: 0x0000D59C
		private static Dictionary<string, Type> GetFunctionsFromAssembly(Assembly assembly)
		{
			Type functionType = typeof(Function);
			return (from t in assembly.GetTypes()
				where functionType.IsAssignableFrom(t) && t != functionType
				where !t.IsAbstract
				select t).SelectMany(new Func<Type, IEnumerable<KeyValuePair<string, Type>>>(Env.GetFunctionNames)).ToDictionary((KeyValuePair<string, Type> kvp) => kvp.Key, (KeyValuePair<string, Type> kvp) => kvp.Value);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000F44E File Offset: 0x0000D64E
		private static Dictionary<string, Type> GetCoreFunctions()
		{
			Dictionary<string, Type> functionsFromAssembly = Env.GetFunctionsFromAssembly(Assembly.GetExecutingAssembly());
			functionsFromAssembly["%"] = typeof(CFormatString);
			return functionsFromAssembly;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000F46F File Offset: 0x0000D66F
		private void AddCoreFunctions()
		{
			this._functionTypes = new Dictionary<string, Type>(Env.CoreFunctions);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000F484 File Offset: 0x0000D684
		public virtual Function GetFunction(string name)
		{
			Function function = null;
			name = name.ToLowerInvariant();
			if (this._functionTypes.ContainsKey(name))
			{
				function = (Function)Activator.CreateInstance(this._functionTypes[name]);
				function.Logger = this.Logger;
			}
			return function;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000F4CD File Offset: 0x0000D6CD
		private static IEnumerable<KeyValuePair<string, Type>> GetFunctionNames(Type t)
		{
			string name = t.Name;
			if (name.EndsWith("function", StringComparison.InvariantCultureIgnoreCase))
			{
				name = name.Substring(0, name.Length - 8);
			}
			name = Regex.Replace(name, "\\B[A-Z]", "-$0");
			name = name.ToLowerInvariant();
			yield return new KeyValuePair<string, Type>(name, t);
			if (name.Contains("-"))
			{
				yield return new KeyValuePair<string, Type>(name.Replace("-", ""), t);
			}
			yield break;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		public void AddExtension(Selector selector, Extend extends, Env env)
		{
			using (List<Selector>.Enumerator enumerator = extends.Exact.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Selector extending2 = enumerator.Current;
					Extender extender;
					if ((extender = this._extensions.OfType<ExactExtender>().FirstOrDefault((ExactExtender e) => e.BaseSelector.ToString().Trim() == extending2.ToString().Trim())) == null)
					{
						extender = new ExactExtender(extending2, extends);
						this._extensions.Add(extender);
					}
					extender.AddExtension(selector, env);
				}
			}
			using (List<Selector>.Enumerator enumerator = extends.Partial.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Selector extending = enumerator.Current;
					Extender extender2;
					if ((extender2 = this._extensions.OfType<PartialExtender>().FirstOrDefault((PartialExtender e) => e.BaseSelector.ToString().Trim() == extending.ToString().Trim())) == null)
					{
						extender2 = new PartialExtender(extending, extends);
						this._extensions.Add(extender2);
					}
					extender2.AddExtension(selector, env);
				}
			}
			if (this.Parent != null)
			{
				this.Parent.AddExtension(selector, extends, env);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000F620 File Offset: 0x0000D820
		public void RegisterExtensionsFrom(Env child)
		{
			this._extensions.AddRange(child._extensions);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000F633 File Offset: 0x0000D833
		public IEnumerable<Extender> FindUnmatchedExtensions()
		{
			return this._extensions.Where((Extender e) => !e.IsMatched);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000F660 File Offset: 0x0000D860
		public ExactExtender FindExactExtension(string selection)
		{
			if (this.ExtendMediaScope.Any<Media>())
			{
				ExactExtender exactExtender = this.ExtendMediaScope.Select((Media media) => media.FindExactExtension(selection)).FirstOrDefault((ExactExtender result) => result != null);
				if (exactExtender != null)
				{
					return exactExtender;
				}
			}
			return this._extensions.OfType<ExactExtender>().FirstOrDefault((ExactExtender e) => e.BaseSelector.ToString().Trim() == selection);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		public PartialExtender[] FindPartialExtensions(Context selection)
		{
			if (this.ExtendMediaScope.Any<Media>())
			{
				PartialExtender[] array = this.ExtendMediaScope.Select((Media media) => media.FindPartialExtensions(selection)).FirstOrDefault((PartialExtender[] result) => result.Any<PartialExtender>());
				if (array != null)
				{
					return array;
				}
			}
			return this._extensions.OfType<PartialExtender>().WhereExtenderMatches(selection).ToArray<PartialExtender>();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000F768 File Offset: 0x0000D968
		[Obsolete("This method doesn't return the correct results. Use FindPartialExtensions(Context) instead.", false)]
		public PartialExtender[] FindPartialExtensions(string selection)
		{
			if (this.ExtendMediaScope.Any<Media>())
			{
				PartialExtender[] array = this.ExtendMediaScope.Select((Media media) => media.FindPartialExtensions(selection)).FirstOrDefault((PartialExtender[] result) => result.Any<PartialExtender>());
				if (array != null)
				{
					return array;
				}
			}
			return (from e in this._extensions.OfType<PartialExtender>()
				where selection.Contains(e.BaseSelector.ToString().Trim())
				select e).ToArray<PartialExtender>();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000F7F1 File Offset: 0x0000D9F1
		public override string ToString()
		{
			return this.Frames.Select((Ruleset f) => f.ToString()).JoinStrings(" <- ");
		}

		// Token: 0x040000BE RID: 190
		private Dictionary<string, Type> _functionTypes;

		// Token: 0x040000BF RID: 191
		private readonly List<IPlugin> _plugins;

		// Token: 0x040000C0 RID: 192
		private readonly List<Extender> _extensions;

		// Token: 0x040000D2 RID: 210
		private static readonly Dictionary<string, Type> CoreFunctions = Env.GetCoreFunctions();
	}
}
