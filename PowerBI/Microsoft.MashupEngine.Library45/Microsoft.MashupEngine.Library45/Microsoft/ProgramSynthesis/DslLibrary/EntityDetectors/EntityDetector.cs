using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors
{
	// Token: 0x02000814 RID: 2068
	[Parseable("TryParseFromXML", ParseHumanReadableString = "TryParseHumanReadable")]
	public abstract class EntityDetector : IRenderableLiteral
	{
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002C94 RID: 11412
		public abstract string Name { get; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002C95 RID: 11413
		public abstract int InputMaxLengthLimit { get; }

		// Token: 0x06002C96 RID: 11414
		public abstract IEnumerable<PositionMatch> GetMatches(string input);

		// Token: 0x06002C97 RID: 11415 RVA: 0x0007D9D6 File Offset: 0x0007BBD6
		public virtual bool HasEntity(string input)
		{
			return input.Length <= this.InputMaxLengthLimit && !string.IsNullOrEmpty(input) && this.GetMatches(input).IsAny<PositionMatch>();
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x0007D9FC File Offset: 0x0007BBFC
		public string RenderHumanReadable()
		{
			return this.Name.ToLiteral(null);
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x0007DA0A File Offset: 0x0007BC0A
		public XElement RenderXML()
		{
			return new XElement("EntityDetector", new XAttribute("Name", this.Name));
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x0007DA30 File Offset: 0x0007BC30
		public static EntityDetector TryParseFromXML(XElement literal, DeserializationContext context)
		{
			EntityDetectorsMap entityDetectorsMap;
			EntityDetector entityDetector;
			if (literal.Name == "EntityDetector" && literal.Attributes("Name").Count<XAttribute>() == 1 && context.TryGetValue<EntityDetectorsMap>(out entityDetectorsMap) && entityDetectorsMap.EmployedEntityDetectors.TryGetValue(literal.Attribute("Name").Value, out entityDetector))
			{
				return entityDetector;
			}
			throw new Exception(string.Format("Unable to parse/load {0} entity", "Name"));
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x0007DAB4 File Offset: 0x0007BCB4
		public static EntityDetector TryParseHumanReadable(string literal, DeserializationContext context)
		{
			EntityDetectorsMap entityDetectorsMap;
			EntityDetector entityDetector;
			if (context.TryGetValue<EntityDetectorsMap>(out entityDetectorsMap) && entityDetectorsMap.EmployedEntityDetectors.TryGetValue(StdLiteralParsing.TryParse<string>(literal, context).Value, out entityDetector))
			{
				return entityDetector;
			}
			throw new Exception(string.Format("Unable to parse/load {0} entity", "Name"));
		}
	}
}
