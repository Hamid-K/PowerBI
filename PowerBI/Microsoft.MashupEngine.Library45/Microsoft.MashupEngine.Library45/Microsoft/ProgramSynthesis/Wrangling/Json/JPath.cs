using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Json
{
	// Token: 0x02000184 RID: 388
	[Parseable("ParseFromXML", ParseHumanReadableString = "ParseFromHumanReadable")]
	public class JPath : IRenderableLiteral, IEquatable<JPath>
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x00019ACE File Offset: 0x00017CCE
		public JPath()
		{
			this.Steps = new JPathStep[0];
			base..ctor();
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00019AE2 File Offset: 0x00017CE2
		public JPath(params JPathStep[] steps)
			: this(steps.AsEnumerable<JPathStep>())
		{
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00019AF0 File Offset: 0x00017CF0
		public JPath(IEnumerable<JPathStep> steps)
		{
			this.Steps = new JPathStep[0];
			base..ctor();
			List<JPathStep> list = new List<JPathStep>();
			foreach (JPathStep jpathStep in steps)
			{
				if (jpathStep == null)
				{
					throw new ArgumentException("A step cannot be null", "steps");
				}
				if (jpathStep.Kind != JPathStepKind.Current)
				{
					list.Add(jpathStep);
				}
			}
			JPathStep[] array;
			if (list.Count <= 0)
			{
				(array = new JPathStep[1])[0] = new CurrentStep();
			}
			else
			{
				array = list.ToArray();
			}
			this.Steps = array;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00019B94 File Offset: 0x00017D94
		public JPath(params string[] steps)
		{
			Func<string, JPathStep> func;
			if ((func = JPath.<>O.<0>__From) == null)
			{
				func = (JPath.<>O.<0>__From = new Func<string, JPathStep>(JPathStep.From));
			}
			this..ctor(steps.Select(func));
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00019B94 File Offset: 0x00017D94
		public JPath(IEnumerable<string> steps)
		{
			Func<string, JPathStep> func;
			if ((func = JPath.<>O.<0>__From) == null)
			{
				func = (JPath.<>O.<0>__From = new Func<string, JPathStep>(JPathStep.From));
			}
			this..ctor(steps.Select(func));
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00019BBD File Offset: 0x00017DBD
		public JPathStep[] Steps { get; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00019BC8 File Offset: 0x00017DC8
		public double Score
		{
			get
			{
				if (this._score == null)
				{
					this._score = new double?(this.Steps.Sum((JPathStep s) => s.Score));
				}
				return this._score.Value;
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00019C22 File Offset: 0x00017E22
		public static JPath Join(params JPath[] paths)
		{
			return new JPath(paths.SelectMany((JPath p) => p.Steps));
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00019C50 File Offset: 0x00017E50
		public static JPath GetPath(JToken src, JToken dst)
		{
			if (src == dst)
			{
				return new JPath(new JPathStep[]
				{
					new CurrentStep()
				});
			}
			HashSet<JToken> hashSet = dst.AncestorsAndSelf().ConvertToHashSet<JToken>();
			List<JPathStep> list = new List<JPathStep>();
			JToken jtoken = src;
			while (jtoken != null && !hashSet.Contains(jtoken))
			{
				list.Add(new ParentStep());
				jtoken = jtoken.Parent;
			}
			if (jtoken == null)
			{
				return null;
			}
			if (jtoken == dst)
			{
				return new JPath(list.ToArray());
			}
			List<JPathStep> list2 = new List<JPathStep>();
			JToken jtoken2 = dst;
			JToken jtoken3 = dst.Parent;
			while (jtoken3 != null && jtoken2 != jtoken)
			{
				switch (jtoken3.Type)
				{
				case JTokenType.Object:
				{
					JProperty jproperty = jtoken2 as JProperty;
					if (jproperty == null || jproperty.Parent != jtoken3)
					{
						return null;
					}
					list2.Insert(0, new AccessStep(jproperty.Name));
					break;
				}
				case JTokenType.Array:
				{
					int num = (jtoken3 as JArray).IndexOf(jtoken2);
					if (num == -1)
					{
						return null;
					}
					list2.Insert(0, new IndexStep(num));
					break;
				}
				}
				jtoken2 = jtoken3;
				jtoken3 = jtoken3.Parent;
			}
			return new JPath(list.Concat(list2).ToArray<JPathStep>());
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00019D76 File Offset: 0x00017F76
		public string RenderHumanReadable()
		{
			return this.Serialize();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00019D7E File Offset: 0x00017F7E
		public XElement RenderXML()
		{
			return new XElement("JPath", this.Serialize());
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00019D98 File Offset: 0x00017F98
		public static JPath ParseFromXML(XElement pathElement)
		{
			JPath jpath;
			try
			{
				jpath = ((pathElement.Name != "JPath") ? null : JPath.ParseFromHumanReadable(pathElement.Value));
			}
			catch
			{
				jpath = null;
			}
			return jpath;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00019DE4 File Offset: 0x00017FE4
		public static JPath ParseFromHumanReadable(string path)
		{
			if (string.IsNullOrEmpty(path) || path.Length < 2)
			{
				return null;
			}
			if (path.First<char>() != '"' && path.Last<char>() != '"')
			{
				return null;
			}
			string text = path.Substring(1, path.Length - 2);
			if (string.IsNullOrWhiteSpace(text))
			{
				return new JPath();
			}
			string[] array = text.Split(new char[] { '/' });
			List<JPathStep> list = new List<JPathStep>(array.Length);
			Func<string, JPathStep> func;
			if ((func = JPath.<>O.<0>__From) == null)
			{
				func = (JPath.<>O.<0>__From = new Func<string, JPathStep>(JPathStep.From));
			}
			foreach (JPathStep jpathStep in array.Select(func))
			{
				if (jpathStep == null)
				{
					return null;
				}
				list.Add(jpathStep);
			}
			return new JPath(list);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00019EC0 File Offset: 0x000180C0
		public string Serialize()
		{
			string text = "\"{0}\"";
			object[] array = new object[1];
			array[0] = string.Join("/", this.Steps.Select((JPathStep s) => s.Serialize()));
			return FormattableString.Invariant(FormattableStringFactory.Create(text, array));
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00019D76 File Offset: 0x00017F76
		public override string ToString()
		{
			return this.Serialize();
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00019F19 File Offset: 0x00018119
		public bool Equals(JPath other)
		{
			return other != null && (this == other || this.Steps.SequenceEqual(other.Steps));
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00019F37 File Offset: 0x00018137
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((JPath)obj)));
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00019F68 File Offset: 0x00018168
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			this._hashCode = new int?(this.Steps.Aggregate(19, (int current, JPathStep step) => current * 31 + step.GetHashCode()));
			return this._hashCode.Value;
		}

		// Token: 0x0400042F RID: 1071
		private double? _score;

		// Token: 0x04000431 RID: 1073
		private int? _hashCode;

		// Token: 0x02000185 RID: 389
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000432 RID: 1074
			public static Func<string, JPathStep> <0>__From;
		}
	}
}
