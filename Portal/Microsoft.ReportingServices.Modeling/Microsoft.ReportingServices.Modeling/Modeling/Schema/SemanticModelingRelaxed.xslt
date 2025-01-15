<?xml version="1.0" encoding="utf-8" ?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <xsl:template name="CopyContent">
        <xsl:apply-templates select="@*|node()"/>
    </xsl:template>
    <xsl:template name="AllowEmptyString">
        <xsl:copy>
            <xsl:call-template name="CopyContent" />
            <xsl:attribute name="type">xsd:string</xsl:attribute>
        </xsl:copy>
    </xsl:template>
    <xsl:template name="OptionalElement">
        <xsl:copy>
            <xsl:call-template name="CopyContent" />
            <xsl:attribute name="minOccurs">0</xsl:attribute>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:call-template name="CopyContent" />
        </xsl:copy>
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:element[@name='SemanticModel']/xsd:complexType/xsd:all/xsd:element[@name='Name']">
        <xsl:call-template name="AllowEmptyString" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='EntityFolderType']/xsd:all/xsd:element[@name='Name']">
        <xsl:call-template name="AllowEmptyString" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='EntityType']/xsd:all/xsd:element[@name='Name']">
        <xsl:call-template name="AllowEmptyString" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='EntityType']/xsd:all/xsd:element[@name='IdentifyingAttributes']">
        <xsl:call-template name="OptionalElement" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='FieldFolderType']/xsd:all/xsd:element[@name='Name']">
        <xsl:call-template name="AllowEmptyString" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='AttributeType']/xsd:all/xsd:element[@name='Name']">
        <xsl:call-template name="AllowEmptyString" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='RoleType']/xsd:all/xsd:element[@name='Name']">
        <xsl:call-template name="AllowEmptyString" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='RoleType']/xsd:all/xsd:element[@name='RelatedRoleID']">
        <xsl:call-template name="OptionalElement" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='PerspectiveType']/xsd:all/xsd:element[@name='Name']">
        <xsl:call-template name="AllowEmptyString" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='PerspectiveType']/xsd:all/xsd:element[@name='ModelItems']">
        <xsl:call-template name="OptionalElement" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='HierarchyType']/xsd:all/xsd:element[@name='BaseEntity']">
        <xsl:call-template name="OptionalElement" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='GroupingType']/xsd:all/xsd:element[@name='Expression']">
        <xsl:call-template name="OptionalElement" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='GroupingType']/xsd:attribute[@name='Name']">
        <xsl:call-template name="AllowEmptyString" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='MeasureGroupType']/xsd:all/xsd:element[@name='BaseEntity']">
        <xsl:call-template name="OptionalElement" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='MeasureGroupType']/xsd:all/xsd:element[@name='Measures']">
        <xsl:call-template name="OptionalElement" />
    </xsl:template>
    <xsl:template match="/xsd:schema/xsd:complexType[@name='ParameterType']/xsd:attribute[@name='Name']">
        <xsl:call-template name="AllowEmptyString" />
    </xsl:template>
</xsl:transform>
