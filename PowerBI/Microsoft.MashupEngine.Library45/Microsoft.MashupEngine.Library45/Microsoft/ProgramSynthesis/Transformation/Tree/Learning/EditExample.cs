using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Transformation.Tree.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Learning
{
	// Token: 0x02001EB5 RID: 7861
	public class EditExample : TTreeExample<State, Edit>
	{
		// Token: 0x06010975 RID: 67957 RVA: 0x00390855 File Offset: 0x0038EA55
		public EditExample(State input, Edit output, bool isPositive = true)
			: base(input, output, isPositive)
		{
		}

		// Token: 0x06010976 RID: 67958 RVA: 0x00390860 File Offset: 0x0038EA60
		internal XElement SerializeToXml(Dictionary<object, int> identityCache, SpecSerializationContext serializers)
		{
			XElement xelement = base.Input.SerializeToXML(identityCache, serializers);
			XElement xelement2 = base.Output.SerializeToXml();
			return new XElement("EditExample", new object[] { xelement, xelement2 }).WithAttribute("IsPositive", base.IsPositive);
		}

		// Token: 0x06010977 RID: 67959 RVA: 0x003908BC File Offset: 0x0038EABC
		internal static EditExample DeserializeFromXml(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			List<XElement> list = node.Elements().ToList<XElement>();
			if (list.Count != 2)
			{
				throw new Exception("Invalid number of elements.");
			}
			State state = State.DeserializeFromXML(list[0], context, identityCache);
			Edit edit = Update.DeserializeFromXml(list[1]);
			Node.AddNonSerializableInfoToTree(edit.Target);
			Node.AddNonSerializableInfoToTree(edit.NewNode);
			XAttribute xattribute = node.Attribute("IsPositive");
			if (xattribute == null)
			{
				throw new Exception("Attribute not found.");
			}
			return new EditExample(state, edit, bool.Parse(xattribute.Value));
		}

		// Token: 0x04006326 RID: 25382
		private const string XName = "EditExample";
	}
}
