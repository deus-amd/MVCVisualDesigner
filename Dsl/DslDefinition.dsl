﻿<?xml version="1.0" encoding="utf-8"?>
<Dsl xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="8f2ca638-e08f-4b6f-96a1-ea046a25c190" Description="An open source visual designer for ASP.NET MVC framework. (Prototype version, and more features will be implemented soon)" Name="MVCVisualDesigner" DisplayName="MVC Visual Designer" Namespace="MVCVisualDesigner" MajorVersion="0" Build="2" ProductName="MVC Visual Designer" CompanyName="Jun Wang" PackageGuid="2318dda0-8eed-4398-b67d-2e85e627224d" PackageNamespace="MVCVisualDesigner" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Classes>
    <DomainClass Id="404ac32b-b3af-4662-bd2e-14f13a17562b" Description="Description for MVCVisualDesigner.VDWidget" Name="VDWidget" DisplayName="VDWidget" InheritanceModifier="Abstract" Namespace="MVCVisualDesigner" GeneratesDoubleDerived="true">
      <Properties>
        <DomainProperty Id="16811f57-2684-4aa9-8695-4f5e2435b04e" Description="" Name="MoreHTMLAttributes" DisplayName="MoreHTMLAttributes" DefaultValue="" Kind="CustomStorage" Category="Custom HTML Attribute">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.EditorAttribute">
              <Parameters>
                <AttributeParameter Value="typeof(MVCVisualDesigner.VDCollectionEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System.Collections.Generic/List&lt;MVCVisualDesigner.HTMLAttribute&gt;" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="VDWidget" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>WidgetHasChildren.Children</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="VDWidgetTitle" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>WidgetHasTitle.Title</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective UsesCustomMerge="true">
          <Index>
            <DomainClassMoniker Name="VDSection" />
          </Index>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="7c91a0ff-d5ec-467a-b0b8-705ad9049330" Description="Description for MVCVisualDesigner.VDView" Name="VDView" DisplayName="VDView" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="db6a631c-6a2e-494c-b56d-a4f048c50743" Description="" Name="VDSection" DisplayName="Section" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="VDSectionHead" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>SectionHasHead.Head</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="VDSectionBody" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>SectionHasBody.Body</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="f9e27ebe-1427-48d9-bba0-b1dfa993ee87" Description="Description for MVCVisualDesigner.VDWidgetTitle" Name="VDWidgetTitle" DisplayName="VDWidget Title" Namespace="MVCVisualDesigner" />
    <DomainClass Id="c5bcbb13-6519-4896-9e8e-2cda89a5c3f6" Description="" Name="VDSectionHead" DisplayName="Section Head" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="b39ae87a-6919-4e1d-a310-83dda7754090" Description="" Name="VDSectionBody" DisplayName="Section Body" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="2500aa65-1d54-460f-a7d5-278035a0f8e3" Description="Description for MVCVisualDesigner.VDSeparator" Name="VDSeparator" DisplayName="VDSeparator" InheritanceModifier="Abstract" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="c761d8da-76c8-4ae9-a268-8d81a7f9fd69" Description="Description for MVCVisualDesigner.VDHoriSeparator" Name="VDHoriSeparator" DisplayName="VDHori Separator" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDSeparator" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="733dc445-5f13-4eea-8922-2ac8229684e0" Description="" Name="TopMargin" DisplayName="Top Margin">
          <Type>
            <ExternalTypeMoniker Name="/System/Double" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ee89b0a4-c89c-4397-8c71-bfb58a862f2b" Description="" Name="BottomMargin" DisplayName="Bottom Margin">
          <Type>
            <ExternalTypeMoniker Name="/System/Double" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="913509e8-33d0-462f-aa85-bd5f4148d3e7" Description="Description for MVCVisualDesigner.VDVertSeparator" Name="VDVertSeparator" DisplayName="VDVert Separator" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDSeparator" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="b5931904-4f5c-4409-8189-a7abfb03c28c" Description="" Name="LeftMargin" DisplayName="Left Margin">
          <Type>
            <ExternalTypeMoniker Name="/System/Double" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="89c67701-70d5-4090-8a5e-947b33f38e17" Description="" Name="RightMargin" DisplayName="Right Margin">
          <Type>
            <ExternalTypeMoniker Name="/System/Double" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="e1d13ded-42d6-4a10-b240-74e54767ea36" Description="Description for MVCVisualDesigner.VDForm" Name="VDForm" DisplayName="VDForm" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="103d62c4-78cd-4e72-9346-a3f9dd2941af" Description="Specifies the name of a form" Name="Name" DisplayName="name" Category="HTML Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="cb742391-b33e-4946-b1e6-88f6fd2c92b2" Description="Specifies where to send the form-data when a form is submitted" Name="Action" DisplayName="action" Category="HTML Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6bede762-2d29-4de9-90b8-214471e1bebe" Description="Specifies the HTTP method to use when sending form-data" Name="Method" DisplayName="method" Category="HTML Attribute">
          <Type>
            <DomainEnumerationMoniker Name="E_HTTPRequestMethod" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0047a667-f98d-46b8-b9bf-47c2d1c95ade" Description="Specifies where to display the response that is received after submitting the form" Name="Target" DisplayName="target" Category="HTML Attribute">
          <Type>
            <DomainEnumerationMoniker Name="E_FormTarget" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d2dde7a1-4c92-4bad-bfb2-97f90fcc099a" Description="Specifies how the form-data should be encoded when submitting it to the server (only for method=&quot;post&quot;)" Name="EncType" DisplayName="enctype" Category="HTML Attribute">
          <Type>
            <DomainEnumerationMoniker Name="E_FormEncType" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="d05ac9e5-6c3f-402b-a967-1e48ff769811" Description="Description for MVCVisualDesigner.VDSubmit" Name="VDSubmit" DisplayName="VDSubmit" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDInput" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="ba0a564c-ece1-4141-8024-9b50274b7e8a" Description="Description for MVCVisualDesigner.VDRadio" Name="VDRadio" DisplayName="VDRadio" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDInput" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="b70cd577-5d7d-42ce-bec7-50a936feb032" Description="Description for MVCVisualDesigner.VDCheckBox" Name="VDCheckBox" DisplayName="VDCheck Box" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDInput" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="e0dbe157-96a1-4519-9d74-082478bddf00" Description="Description for MVCVisualDesigner.VDSelect" Name="VDSelect" DisplayName="VDSelect" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="c3239d80-aecf-4608-a9d7-bc139fba1563" Description="Defines a name for the drop-down list" Name="Name" DisplayName="name" Category="HTML Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1f17ad15-707c-4653-a778-8686df3bcf94" Description="Specifies that a drop-down list should be disabled" Name="Disabled" DisplayName="disabled" Category="HTML Attribute">
          <Type>
            <DomainEnumerationMoniker Name="E_TripleState" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="25d7186f-4e8e-4f04-bf4f-f126ec8c705e" Description=" Specifies that multiple options can be selected at once" Name="Multiple" DisplayName="multiple" Category="HTML Attribute">
          <Type>
            <DomainEnumerationMoniker Name="E_TripleState" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="55d51255-91d8-40cb-b4cc-51d277232dd8" Description="Defines the number of visible options in a drop-down list" Name="Size" DisplayName="size" Category="HTML Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/UInt32" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="VDSelectOption" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>SelectHasOptions.Options</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="576f41c9-9810-4cad-bf99-ee63eccf6822" Description="Description for MVCVisualDesigner.VDInput" Name="VDInput" DisplayName="VDInput" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="1f32c526-411b-42b1-b3cc-0cf125a81036" Description="Specifies the name of an &lt;input&gt; element" Name="Name" DisplayName="name" Category="HTML Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="57e39616-b2b8-40a2-9f5b-14e8661d3043" Description="Specifies the type &lt;input&gt; element to display" Name="Type" DisplayName="type" Category="HTML Attribute">
          <Type>
            <DomainEnumerationMoniker Name="E_InputType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="909eba16-a948-426d-b86b-3f287ca9e259" Description="Specifies the value of an &lt;input&gt; element" Name="Value" DisplayName="value" Category="HTML Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e184679e-8ac6-4d73-b9c7-1022c63a54a8" Description="Specifies the maximum number of characters allowed in an &lt;input&gt; element" Name="MaxLength" DisplayName="maxlength" Category="HTML Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/UInt32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3741f758-502a-4f14-a73b-d663f16583bb" Description="Specifies the types of files that the server accepts (only for type=&quot;file&quot;)" Name="Accept" DisplayName="accept" Category="HTML Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a994bbc4-7d59-4917-aa81-ddcdfc04a08d" Description="Specifies an alternate text for images (only for type=&quot;image&quot;)" Name="Alt" DisplayName="alt" Category="HTML Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="173cf5c2-e89c-4600-b37c-f7288c573586" Description="Specifies the width, in characters, of an &lt;input&gt; element" Name="Size" DisplayName="size" Category="HTML Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/UInt32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3b058df9-e96e-4412-93e6-506aa022e255" Description="Specifies that an &lt;input&gt; element should be pre-selected when the page loads (for type=&quot;checkbox&quot; or type=&quot;radio&quot;)" Name="Checked" DisplayName="checked" Category="HTML Attribute">
          <Type>
            <DomainEnumerationMoniker Name="E_TripleState" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2a73c5e2-e71d-4b66-b77a-3768982d80fc" Description="Specifies that an &lt;input&gt; element should be disabled" Name="Disabled" DisplayName="disabled" Category="HTML Attribute">
          <Type>
            <DomainEnumerationMoniker Name="E_TripleState" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="7efdaa9f-896e-40b4-8028-706bb6d7b001" Description="Description for MVCVisualDesigner.VDSelectOption" Name="VDSelectOption" DisplayName="VDSelect Option" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="bbd20202-1e1b-4c95-97ff-650e7932b631" Description="Specifies the value to be sent to a server" Name="Value" DisplayName="value" Category="HTML Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2283af80-b312-4ed9-94f2-a5e8dc84644d" Description="Specifies that an option should be pre-selected when the page loads" Name="Selected" DisplayName="selected" Category="HTML Attribute">
          <Type>
            <DomainEnumerationMoniker Name="E_TripleState" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7910839d-f88b-4fc5-8ff3-72898795f093" Description="Specifies a shorter label for an option" Name="Label" DisplayName="label" Category="HTML Attribute">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d8c29f2a-b3ab-4fbb-ad0d-1b973cded29f" Description="Specifies that an option should be disabled" Name="Disabled" DisplayName="disabled" Category="HTML Attribute">
          <Type>
            <DomainEnumerationMoniker Name="E_TripleState" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
  </Classes>
  <Relationships>
    <DomainRelationship Id="8491408d-7160-4a47-9979-e0dab01d52b3" Description="Description for MVCVisualDesigner.WidgetHasChildren" Name="WidgetHasChildren" DisplayName="Widget Has Children" Namespace="MVCVisualDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="223507db-ce6c-4fe1-87aa-33d1adcb6555" Description="Description for MVCVisualDesigner.WidgetHasChildren.SourceVDWidget" Name="SourceVDWidget" DisplayName="Source VDWidget" PropertyName="Children" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Children">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="c0aff184-c06b-475e-b78b-0854c9f15199" Description="Description for MVCVisualDesigner.WidgetHasChildren.TargetVDWidget" Name="TargetVDWidget" DisplayName="Target VDWidget" PropertyName="Parent" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Parent">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="c2d0e6ff-0c0d-4a7d-8ca7-94d2bc2a088c" Description="Description for MVCVisualDesigner.WidgetHasTitle" Name="WidgetHasTitle" DisplayName="Widget Has Title" Namespace="MVCVisualDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="bdb03725-26b0-460b-92ad-c74d0e68d858" Description="Description for MVCVisualDesigner.WidgetHasTitle.VDWidget" Name="VDWidget" DisplayName="VDWidget" PropertyName="Title" Multiplicity="ZeroOne" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Title">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3e997446-1b1d-4306-b258-b373deba3ada" Description="Description for MVCVisualDesigner.WidgetHasTitle.VDWidgetTitle" Name="VDWidgetTitle" DisplayName="VDWidget Title" PropertyName="Widget" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Widget">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidgetTitle" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="60c511f9-a11f-40e7-9cab-542425f90fe3" Description="Description for MVCVisualDesigner.SectionHasHead" Name="SectionHasHead" DisplayName="Section Has Head" Namespace="MVCVisualDesigner" IsEmbedding="true">
      <BaseRelationship>
        <DomainRelationshipMoniker Name="WidgetHasChildren" />
      </BaseRelationship>
      <Source>
        <DomainRole Id="b5841c7c-52eb-4e63-8d04-5826d66991fb" Description="Description for MVCVisualDesigner.SectionHasHead.VDSection" Name="VDSection" DisplayName="VDSection" PropertyName="Head" Multiplicity="ZeroOne" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Head">
          <RolePlayer>
            <DomainClassMoniker Name="VDSection" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3d2e888c-20b9-4c74-83ba-13231124b279" Description="Description for MVCVisualDesigner.SectionHasHead.VDSectionHead" Name="VDSectionHead" DisplayName="VDSection Head" PropertyName="Section" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Section">
          <RolePlayer>
            <DomainClassMoniker Name="VDSectionHead" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="2003e7b1-fa16-47a1-99a1-61d79247a057" Description="Description for MVCVisualDesigner.SectionHasBody" Name="SectionHasBody" DisplayName="Section Has Body" Namespace="MVCVisualDesigner" IsEmbedding="true">
      <BaseRelationship>
        <DomainRelationshipMoniker Name="WidgetHasChildren" />
      </BaseRelationship>
      <Source>
        <DomainRole Id="022e1990-52a9-48bc-ab8a-9c7bb72f9367" Description="Description for MVCVisualDesigner.SectionHasBody.VDSection" Name="VDSection" DisplayName="VDSection" PropertyName="Body" Multiplicity="ZeroOne" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Body">
          <RolePlayer>
            <DomainClassMoniker Name="VDSection" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="988773ea-649c-4f8d-b7e2-05a5c4f55edd" Description="Description for MVCVisualDesigner.SectionHasBody.VDSectionBody" Name="VDSectionBody" DisplayName="VDSection Body" PropertyName="Section" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Section">
          <RolePlayer>
            <DomainClassMoniker Name="VDSectionBody" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="ab5a4903-7e60-4c67-a237-5f61f0654e46" Description="Description for MVCVisualDesigner.SeparatorRefersTopWidget" Name="SeparatorRefersTopWidget" DisplayName="Separator Refers Top Widget" Namespace="MVCVisualDesigner">
      <Source>
        <DomainRole Id="3d142754-17c4-448b-aaa3-261fd13e25c5" Description="Description for MVCVisualDesigner.SeparatorRefersTopWidget.VDHoriSeparator" Name="VDHoriSeparator" DisplayName="VDHori Separator" PropertyName="TopWidget" Multiplicity="ZeroOne" PropertyDisplayName="Top Widget">
          <RolePlayer>
            <DomainClassMoniker Name="VDHoriSeparator" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="968b8f56-745c-4610-b37e-0e34a8060fff" Description="Description for MVCVisualDesigner.SeparatorRefersTopWidget.VDWidget" Name="VDWidget" DisplayName="VDWidget" PropertyName="BottomSeparator" Multiplicity="ZeroOne" PropertyDisplayName="Bottom Separator">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="5fa5d01c-b9ce-45dc-8a43-60139b0e0c7b" Description="Description for MVCVisualDesigner.SeparatorRefersBottomWidget" Name="SeparatorRefersBottomWidget" DisplayName="Separator Refers Bottom Widget" Namespace="MVCVisualDesigner">
      <Source>
        <DomainRole Id="0411c536-af87-467a-b66a-173791f724d0" Description="Description for MVCVisualDesigner.SeparatorRefersBottomWidget.VDHoriSeparator" Name="VDHoriSeparator" DisplayName="VDHori Separator" PropertyName="BottomWidget" Multiplicity="ZeroOne" PropertyDisplayName="Bottom Widget">
          <RolePlayer>
            <DomainClassMoniker Name="VDHoriSeparator" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="6ba23e40-16ee-47d5-a195-ee3939bbcacd" Description="Description for MVCVisualDesigner.SeparatorRefersBottomWidget.VDWidget" Name="VDWidget" DisplayName="VDWidget" PropertyName="TopSeparator" Multiplicity="ZeroOne" PropertyDisplayName="Top Separator">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="2d6b812d-fc42-4b4c-99c5-a66e10169b1a" Description="Description for MVCVisualDesigner.SeparatorRefersLeftWidget" Name="SeparatorRefersLeftWidget" DisplayName="Separator Refers Left Widget" Namespace="MVCVisualDesigner">
      <Source>
        <DomainRole Id="975d491c-2e71-4131-bf82-a4dcc85fc012" Description="Description for MVCVisualDesigner.SeparatorRefersLeftWidget.VDVertSeparator" Name="VDVertSeparator" DisplayName="VDVert Separator" PropertyName="LeftWidget" Multiplicity="ZeroOne" PropertyDisplayName="Left Widget">
          <RolePlayer>
            <DomainClassMoniker Name="VDVertSeparator" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="16ddd36d-f8d5-4e77-9a9f-0e6a2a54cbce" Description="Description for MVCVisualDesigner.SeparatorRefersLeftWidget.VDWidget" Name="VDWidget" DisplayName="VDWidget" PropertyName="RightSeparator" Multiplicity="ZeroOne" PropertyDisplayName="Right Separator">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="f47ff5c7-3744-4c53-af5c-c48cc1f348bc" Description="Description for MVCVisualDesigner.SeparatorRefersRightWidget" Name="SeparatorRefersRightWidget" DisplayName="Separator Refers Right Widget" Namespace="MVCVisualDesigner">
      <Source>
        <DomainRole Id="18c0cf98-a4df-48da-bb8a-4735141d8d42" Description="Description for MVCVisualDesigner.SeparatorRefersRightWidget.VDVertSeparator" Name="VDVertSeparator" DisplayName="VDVert Separator" PropertyName="RightWidget" Multiplicity="ZeroOne" PropertyDisplayName="Right Widget">
          <RolePlayer>
            <DomainClassMoniker Name="VDVertSeparator" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="81a7b620-7815-4c64-b004-154cb2f1804d" Description="Description for MVCVisualDesigner.SeparatorRefersRightWidget.VDWidget" Name="VDWidget" DisplayName="VDWidget" PropertyName="LeftSeparator" Multiplicity="ZeroOne" PropertyDisplayName="Left Separator">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="c6907723-7e30-45fa-9253-ecee54066a5a" Description="Description for MVCVisualDesigner.SelectHasOptions" Name="SelectHasOptions" DisplayName="Select Has Options" Namespace="MVCVisualDesigner" IsEmbedding="true">
      <BaseRelationship>
        <DomainRelationshipMoniker Name="WidgetHasChildren" />
      </BaseRelationship>
      <Source>
        <DomainRole Id="e019cb3f-1dc3-41a0-b942-cf659ef498cc" Description="Description for MVCVisualDesigner.SelectHasOptions.VDSelect" Name="VDSelect" DisplayName="VDSelect" PropertyName="Options" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Options">
          <RolePlayer>
            <DomainClassMoniker Name="VDSelect" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="0e559dc2-8811-4db7-bd54-fd23feda8009" Description="Description for MVCVisualDesigner.SelectHasOptions.VDSelectOption" Name="VDSelectOption" DisplayName="VDSelect Option" PropertyName="Select" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Select">
          <RolePlayer>
            <DomainClassMoniker Name="VDSelectOption" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
  </Relationships>
  <Types>
    <ExternalType Name="DateTime" Namespace="System" />
    <ExternalType Name="String" Namespace="System" />
    <ExternalType Name="Int16" Namespace="System" />
    <ExternalType Name="Int32" Namespace="System" />
    <ExternalType Name="Int64" Namespace="System" />
    <ExternalType Name="UInt16" Namespace="System" />
    <ExternalType Name="UInt32" Namespace="System" />
    <ExternalType Name="UInt64" Namespace="System" />
    <ExternalType Name="SByte" Namespace="System" />
    <ExternalType Name="Byte" Namespace="System" />
    <ExternalType Name="Double" Namespace="System" />
    <ExternalType Name="Single" Namespace="System" />
    <ExternalType Name="Guid" Namespace="System" />
    <ExternalType Name="Boolean" Namespace="System" />
    <ExternalType Name="Char" Namespace="System" />
    <ExternalType Name="Image" Namespace="System.Drawing" />
    <DomainEnumeration Name="E_HTTPRequestMethod" Namespace="MVCVisualDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="Get" Value="0" />
        <EnumerationLiteral Description="" Name="Post" Value="1" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="E_FormTarget" Namespace="MVCVisualDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="_blank" Value="1" />
        <EnumerationLiteral Description="" Name="_self" Value="3" />
        <EnumerationLiteral Description="" Name="_parent" Value="2" />
        <EnumerationLiteral Description="" Name="_top" Value="4" />
        <EnumerationLiteral Description="Description for MVCVisualDesigner.E_FormTarget.NotSpecified" Name="NotSpecified" Value="0" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="E_FormEncType" Namespace="MVCVisualDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="application/x-www-form-urlencoded" Name="application_x_www_form_urlencoded" Value="1" />
        <EnumerationLiteral Description="multipart/form-data" Name="multipart_form_data" Value="2" />
        <EnumerationLiteral Description="text/plain" Name="text_plain" Value="3" />
        <EnumerationLiteral Description="" Name="NotSpecified" Value="0" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="E_InputType" Namespace="MVCVisualDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="button" Value="" />
        <EnumerationLiteral Description="" Name="checkbox" Value="" />
        <EnumerationLiteral Description="" Name="color" Value="" />
        <EnumerationLiteral Description="" Name="date" Value="" />
        <EnumerationLiteral Description="" Name="file" Value="" />
        <EnumerationLiteral Description="" Name="image" Value="" />
        <EnumerationLiteral Description="" Name="month" Value="" />
        <EnumerationLiteral Description="" Name="number" Value="" />
        <EnumerationLiteral Description="" Name="password" Value="" />
        <EnumerationLiteral Description="" Name="radio" Value="" />
        <EnumerationLiteral Description="" Name="range" Value="" />
        <EnumerationLiteral Description="" Name="reset" Value="" />
        <EnumerationLiteral Description="" Name="datetime" Value="" />
        <EnumerationLiteral Description="datetime-local" Name="datetime_local" Value="" />
        <EnumerationLiteral Description="" Name="email" Value="" />
        <EnumerationLiteral Description="" Name="hidden" Value="" />
        <EnumerationLiteral Description="" Name="search" Value="" />
        <EnumerationLiteral Description="" Name="submit" Value="" />
        <EnumerationLiteral Description="" Name="text" Value="" />
        <EnumerationLiteral Description="" Name="time" Value="" />
        <EnumerationLiteral Description="" Name="tel" Value="" />
        <EnumerationLiteral Description="" Name="url" Value="" />
        <EnumerationLiteral Description="" Name="week" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="E_TripleState" Namespace="MVCVisualDesigner" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="TRUE" Value="2" />
        <EnumerationLiteral Description="" Name="FALSE" Value="1" />
        <EnumerationLiteral Description="" Name="NONE" Value="0" />
      </Literals>
    </DomainEnumeration>
    <ExternalType Name="Dictionary&lt;System.String, System.String&gt;" Namespace="System.Collections.Generic" />
    <ExternalType Name="List&lt;MVCVisualDesigner.HTMLAttribute&gt;" Namespace="System.Collections.Generic" />
  </Types>
  <Shapes>
    <GeometryShape Id="e3f0af00-12a6-4223-861d-180f9ed7f7c3" Description="Description for MVCVisualDesigner.VDWidgetShape" Name="VDWidgetShape" DisplayName="VDWidget Shape" InheritanceModifier="Abstract" Namespace="MVCVisualDesigner" FixedTooltipText="VDWidget Shape" InitialHeight="1" Geometry="Rectangle">
      <Properties>
        <DomainProperty Id="58f89d0b-35cb-4f92-aecd-348184bff87c" Description="" Name="disabled" DisplayName="Disabled" Kind="CustomStorage" Category="Internal States" GetterAccessModifier="FamilyOrAssembly" SetterAccessModifier="FamilyOrAssembly">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d974e6e0-adbc-4b2c-bc4d-cce5d6d87290" Description="" Name="isPinned" DisplayName="Is Pinned" Category="Internal States" GetterAccessModifier="FamilyOrAssembly" SetterAccessModifier="FamilyOrAssembly">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="29d8d234-51c6-433f-881f-b5b9b722ad5a" Description="" Name="titleText" DisplayName="Title Text" Kind="Calculated" Category="Internal States" GetterAccessModifier="FamilyOrAssembly" SetterAccessModifier="FamilyOrAssembly">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="dbf4b4ff-482e-4779-b81a-4107eecf2d05" Description="" Name="titleIcon" DisplayName="Title Icon" Kind="Calculated" Category="Internal States" GetterAccessModifier="FamilyOrAssembly" SetterAccessModifier="FamilyOrAssembly">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Image" />
          </Type>
        </DomainProperty>
      </Properties>
    </GeometryShape>
    <GeometryShape Id="4842b8a8-4cb9-429d-ad01-a6d400e0676b" Description="" Name="VDSectionShape" DisplayName="VDSection Shape" Namespace="MVCVisualDesigner" FixedTooltipText="VDSection Shape" InitialWidth="5" InitialHeight="3" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <Port Id="407d285a-e203-4917-a395-730505e148c5" Description="" Name="VDWidgetTitlePort" DisplayName="Widget Title Port" Namespace="MVCVisualDesigner" GeneratesDoubleDerived="true" FixedTooltipText="Widget Title Port" TextColor="White" FillColor="DarkBlue" OutlineColor="DarkBlue" InitialWidth="1" InitialHeight="0.18" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle" />
    <GeometryShape Id="36e435cc-7626-4bb3-a4aa-b529b7cdfea4" Description="" Name="VDSectionHeadShape" DisplayName="VDSection Head Shape" Namespace="MVCVisualDesigner" FixedTooltipText="VDSection Head Shape" FillColor="SkyBlue" OutlineColor="Transparent" InitialWidth="5" InitialHeight="0.3" OutlineThickness="0" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="498d7617-e998-4ceb-9cc7-257e76f1d134" Description="" Name="VDSectionBodyShape" DisplayName="VDSection Body Shape" Namespace="MVCVisualDesigner" FixedTooltipText="VDSection Body Shape" FillColor="Gainsboro" OutlineColor="Transparent" InitialWidth="5" InitialHeight="2.7" OutlineThickness="0" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="b44e3be4-49b9-4846-9947-9cb59c525a31" Description="" Name="VDHoriSeparatorShape" DisplayName="Horizontal Separator Shape" Namespace="MVCVisualDesigner" FixedTooltipText="Horizontal Separator Shape" FillColor="Gray" OutlineColor="Transparent" InitialHeight="0.05" OutlineThickness="0" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="07c13f52-86d8-4883-830a-821b16c4bf78" Description="" Name="VDVertSeparatorShape" DisplayName="Vertical Separator Shape" Namespace="MVCVisualDesigner" FixedTooltipText="Vertical Separator Shape" FillColor="Gray" OutlineColor="Transparent" InitialWidth="0.05" InitialHeight="1" OutlineThickness="0" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="a08a5644-3f25-48f8-8d7d-fda0c5a7e373" Description="" Name="VDFormShape" DisplayName="Form" Namespace="MVCVisualDesigner" FixedTooltipText="Form" FillColor="WhiteSmoke" InitialWidth="3.5" InitialHeight="2" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="0a3b9ee6-7997-482f-83ee-ad395df76b45" Description="" Name="VDInputShape" DisplayName="Input" Namespace="MVCVisualDesigner" FixedTooltipText="Input" InitialHeight="0.2" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="77418c02-9ec5-41a0-9d58-6b1e30010201" Description="" Name="VDSubmitShape" DisplayName="Submit" Namespace="MVCVisualDesigner" FixedTooltipText="Submit" InitialHeight="0.2" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="d80b71df-4097-4d85-9525-e7cbc392ec41" Description="" Name="VDRadioShape" DisplayName="Radio" Namespace="MVCVisualDesigner" FixedTooltipText="Radio" InitialHeight="0.2" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="9813e2e1-3da9-4c54-89ca-cbf64c57bf2a" Description="" Name="VDCheckBoxShape" DisplayName="Check Box" Namespace="MVCVisualDesigner" FixedTooltipText="Check Box" InitialHeight="0.2" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="a79136a8-1983-406a-87d7-faa94cdcd233" Description="" Name="VDSelectShape" DisplayName="Select" Namespace="MVCVisualDesigner" FixedTooltipText="Select" InitialHeight="1" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="f923f0b5-a39f-4a9a-94fc-2f4d373abacc" Description="" Name="VDSelectOptionShape" DisplayName="Select Option" Namespace="MVCVisualDesigner" FixedTooltipText="Select Option" InitialWidth="1.2" InitialHeight="0.2" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
  </Shapes>
  <XmlSerializationBehavior Name="MVCVisualDesignerSerializationBehavior" Namespace="MVCVisualDesigner">
    <ClassData>
      <XmlClassData TypeName="VDDiagram" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDDiagramMoniker" ElementName="vDDiagram" MonikerTypeName="VDDiagramMoniker">
        <DiagramMoniker Name="VDDiagram" />
      </XmlClassData>
      <XmlClassData TypeName="VDWidget" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDWidgetMoniker" ElementName="vDWidget" MonikerTypeName="VDWidgetMoniker">
        <DomainClassMoniker Name="VDWidget" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="children">
            <DomainRelationshipMoniker Name="WidgetHasChildren" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="title">
            <DomainRelationshipMoniker Name="WidgetHasTitle" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="moreHTMLAttributes">
            <DomainPropertyMoniker Name="VDWidget/MoreHTMLAttributes" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="VDView" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDViewMoniker" ElementName="vDView" MonikerTypeName="VDViewMoniker">
        <DomainClassMoniker Name="VDView" />
      </XmlClassData>
      <XmlClassData TypeName="VDSection" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionMoniker" ElementName="vDSection" MonikerTypeName="VDSectionMoniker">
        <DomainClassMoniker Name="VDSection" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="head">
            <DomainRelationshipMoniker Name="SectionHasHead" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="body">
            <DomainRelationshipMoniker Name="SectionHasBody" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="WidgetHasChildren" MonikerAttributeName="" SerializeId="true" MonikerElementName="widgetHasChildrenMoniker" ElementName="widgetHasChildren" MonikerTypeName="WidgetHasChildrenMoniker">
        <DomainRelationshipMoniker Name="WidgetHasChildren" />
      </XmlClassData>
      <XmlClassData TypeName="VDWidgetShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDWidgetShapeMoniker" ElementName="vDWidgetShape" MonikerTypeName="VDWidgetShapeMoniker">
        <GeometryShapeMoniker Name="VDWidgetShape" />
        <ElementData>
          <XmlPropertyData XmlName="disabled">
            <DomainPropertyMoniker Name="VDWidgetShape/disabled" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isPinned">
            <DomainPropertyMoniker Name="VDWidgetShape/isPinned" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="titleText" Representation="Ignore">
            <DomainPropertyMoniker Name="VDWidgetShape/titleText" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="titleIcon" Representation="Ignore">
            <DomainPropertyMoniker Name="VDWidgetShape/titleIcon" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="VDSectionShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionShapeMoniker" ElementName="vDSectionShape" MonikerTypeName="VDSectionShapeMoniker">
        <GeometryShapeMoniker Name="VDSectionShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDWidgetTitle" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDWidgetTitleMoniker" ElementName="vDWidgetTitle" MonikerTypeName="VDWidgetTitleMoniker">
        <DomainClassMoniker Name="VDWidgetTitle" />
      </XmlClassData>
      <XmlClassData TypeName="WidgetHasTitle" MonikerAttributeName="" SerializeId="true" MonikerElementName="widgetHasTitleMoniker" ElementName="widgetHasTitle" MonikerTypeName="WidgetHasTitleMoniker">
        <DomainRelationshipMoniker Name="WidgetHasTitle" />
      </XmlClassData>
      <XmlClassData TypeName="VDWidgetTitlePort" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDWidgetTitlePortMoniker" ElementName="vDWidgetTitlePort" MonikerTypeName="VDWidgetTitlePortMoniker">
        <PortMoniker Name="VDWidgetTitlePort" />
      </XmlClassData>
      <XmlClassData TypeName="VDSectionHead" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionHeadMoniker" ElementName="vDSectionHead" MonikerTypeName="VDSectionHeadMoniker">
        <DomainClassMoniker Name="VDSectionHead" />
      </XmlClassData>
      <XmlClassData TypeName="VDSectionBody" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionBodyMoniker" ElementName="vDSectionBody" MonikerTypeName="VDSectionBodyMoniker">
        <DomainClassMoniker Name="VDSectionBody" />
      </XmlClassData>
      <XmlClassData TypeName="SectionHasHead" MonikerAttributeName="" SerializeId="true" MonikerElementName="sectionHasHeadMoniker" ElementName="sectionHasHead" MonikerTypeName="SectionHasHeadMoniker">
        <DomainRelationshipMoniker Name="SectionHasHead" />
      </XmlClassData>
      <XmlClassData TypeName="SectionHasBody" MonikerAttributeName="" SerializeId="true" MonikerElementName="sectionHasBodyMoniker" ElementName="sectionHasBody" MonikerTypeName="SectionHasBodyMoniker">
        <DomainRelationshipMoniker Name="SectionHasBody" />
      </XmlClassData>
      <XmlClassData TypeName="VDSectionHeadShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionHeadShapeMoniker" ElementName="vDSectionHeadShape" MonikerTypeName="VDSectionHeadShapeMoniker">
        <GeometryShapeMoniker Name="VDSectionHeadShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDSectionBodyShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionBodyShapeMoniker" ElementName="vDSectionBodyShape" MonikerTypeName="VDSectionBodyShapeMoniker">
        <GeometryShapeMoniker Name="VDSectionBodyShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDSeparator" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSeparatorMoniker" ElementName="vDSeparator" MonikerTypeName="VDSeparatorMoniker">
        <DomainClassMoniker Name="VDSeparator" />
      </XmlClassData>
      <XmlClassData TypeName="VDHoriSeparator" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDHoriSeparatorMoniker" ElementName="vDHoriSeparator" MonikerTypeName="VDHoriSeparatorMoniker">
        <DomainClassMoniker Name="VDHoriSeparator" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="topWidget">
            <DomainRelationshipMoniker Name="SeparatorRefersTopWidget" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="bottomWidget">
            <DomainRelationshipMoniker Name="SeparatorRefersBottomWidget" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="topMargin">
            <DomainPropertyMoniker Name="VDHoriSeparator/TopMargin" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="bottomMargin">
            <DomainPropertyMoniker Name="VDHoriSeparator/BottomMargin" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="VDVertSeparator" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDVertSeparatorMoniker" ElementName="vDVertSeparator" MonikerTypeName="VDVertSeparatorMoniker">
        <DomainClassMoniker Name="VDVertSeparator" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="leftWidget">
            <DomainRelationshipMoniker Name="SeparatorRefersLeftWidget" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="rightWidget">
            <DomainRelationshipMoniker Name="SeparatorRefersRightWidget" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="leftMargin">
            <DomainPropertyMoniker Name="VDVertSeparator/LeftMargin" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="rightMargin">
            <DomainPropertyMoniker Name="VDVertSeparator/RightMargin" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="SeparatorRefersTopWidget" MonikerAttributeName="" SerializeId="true" MonikerElementName="separatorRefersTopWidgetMoniker" ElementName="separatorRefersTopWidget" MonikerTypeName="SeparatorRefersTopWidgetMoniker">
        <DomainRelationshipMoniker Name="SeparatorRefersTopWidget" />
      </XmlClassData>
      <XmlClassData TypeName="SeparatorRefersBottomWidget" MonikerAttributeName="" SerializeId="true" MonikerElementName="separatorRefersBottomWidgetMoniker" ElementName="separatorRefersBottomWidget" MonikerTypeName="SeparatorRefersBottomWidgetMoniker">
        <DomainRelationshipMoniker Name="SeparatorRefersBottomWidget" />
      </XmlClassData>
      <XmlClassData TypeName="SeparatorRefersLeftWidget" MonikerAttributeName="" SerializeId="true" MonikerElementName="separatorRefersLeftWidgetMoniker" ElementName="separatorRefersLeftWidget" MonikerTypeName="SeparatorRefersLeftWidgetMoniker">
        <DomainRelationshipMoniker Name="SeparatorRefersLeftWidget" />
      </XmlClassData>
      <XmlClassData TypeName="SeparatorRefersRightWidget" MonikerAttributeName="" SerializeId="true" MonikerElementName="separatorRefersRightWidgetMoniker" ElementName="separatorRefersRightWidget" MonikerTypeName="SeparatorRefersRightWidgetMoniker">
        <DomainRelationshipMoniker Name="SeparatorRefersRightWidget" />
      </XmlClassData>
      <XmlClassData TypeName="VDHoriSeparatorShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDHoriSeparatorShapeMoniker" ElementName="vDHoriSeparatorShape" MonikerTypeName="VDHoriSeparatorShapeMoniker">
        <GeometryShapeMoniker Name="VDHoriSeparatorShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDVertSeparatorShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDVertSeparatorShapeMoniker" ElementName="vDVertSeparatorShape" MonikerTypeName="VDVertSeparatorShapeMoniker">
        <GeometryShapeMoniker Name="VDVertSeparatorShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDForm" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDFormMoniker" ElementName="vDForm" MonikerTypeName="VDFormMoniker">
        <DomainClassMoniker Name="VDForm" />
        <ElementData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="VDForm/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="action">
            <DomainPropertyMoniker Name="VDForm/Action" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="method">
            <DomainPropertyMoniker Name="VDForm/Method" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="target">
            <DomainPropertyMoniker Name="VDForm/Target" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="encType">
            <DomainPropertyMoniker Name="VDForm/EncType" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="VDSubmit" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSubmitMoniker" ElementName="vDSubmit" MonikerTypeName="VDSubmitMoniker">
        <DomainClassMoniker Name="VDSubmit" />
      </XmlClassData>
      <XmlClassData TypeName="VDRadio" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDRadioMoniker" ElementName="vDRadio" MonikerTypeName="VDRadioMoniker">
        <DomainClassMoniker Name="VDRadio" />
      </XmlClassData>
      <XmlClassData TypeName="VDCheckBox" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDCheckBoxMoniker" ElementName="vDCheckBox" MonikerTypeName="VDCheckBoxMoniker">
        <DomainClassMoniker Name="VDCheckBox" />
      </XmlClassData>
      <XmlClassData TypeName="VDSelect" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSelectMoniker" ElementName="vDSelect" MonikerTypeName="VDSelectMoniker">
        <DomainClassMoniker Name="VDSelect" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="options">
            <DomainRelationshipMoniker Name="SelectHasOptions" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="VDSelect/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="disabled">
            <DomainPropertyMoniker Name="VDSelect/Disabled" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="multiple">
            <DomainPropertyMoniker Name="VDSelect/Multiple" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="size">
            <DomainPropertyMoniker Name="VDSelect/Size" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="VDInput" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDInputMoniker" ElementName="vDInput" MonikerTypeName="VDInputMoniker">
        <DomainClassMoniker Name="VDInput" />
        <ElementData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="VDInput/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="VDInput/Type" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="value">
            <DomainPropertyMoniker Name="VDInput/Value" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="maxLength">
            <DomainPropertyMoniker Name="VDInput/MaxLength" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="accept">
            <DomainPropertyMoniker Name="VDInput/Accept" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="alt">
            <DomainPropertyMoniker Name="VDInput/Alt" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="size">
            <DomainPropertyMoniker Name="VDInput/Size" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="checked">
            <DomainPropertyMoniker Name="VDInput/Checked" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="disabled">
            <DomainPropertyMoniker Name="VDInput/Disabled" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="VDSelectOption" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSelectOptionMoniker" ElementName="vDSelectOption" MonikerTypeName="VDSelectOptionMoniker">
        <DomainClassMoniker Name="VDSelectOption" />
        <ElementData>
          <XmlPropertyData XmlName="value">
            <DomainPropertyMoniker Name="VDSelectOption/Value" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="selected">
            <DomainPropertyMoniker Name="VDSelectOption/Selected" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="label">
            <DomainPropertyMoniker Name="VDSelectOption/Label" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="disabled">
            <DomainPropertyMoniker Name="VDSelectOption/Disabled" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="VDFormShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDFormShapeMoniker" ElementName="vDFormShape" MonikerTypeName="VDFormShapeMoniker">
        <GeometryShapeMoniker Name="VDFormShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDInputShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDInputShapeMoniker" ElementName="vDInputShape" MonikerTypeName="VDInputShapeMoniker">
        <GeometryShapeMoniker Name="VDInputShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDSubmitShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSubmitShapeMoniker" ElementName="vDSubmitShape" MonikerTypeName="VDSubmitShapeMoniker">
        <GeometryShapeMoniker Name="VDSubmitShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDRadioShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDRadioShapeMoniker" ElementName="vDRadioShape" MonikerTypeName="VDRadioShapeMoniker">
        <GeometryShapeMoniker Name="VDRadioShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDCheckBoxShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDCheckBoxShapeMoniker" ElementName="vDCheckBoxShape" MonikerTypeName="VDCheckBoxShapeMoniker">
        <GeometryShapeMoniker Name="VDCheckBoxShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDSelectShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSelectShapeMoniker" ElementName="vDSelectShape" MonikerTypeName="VDSelectShapeMoniker">
        <GeometryShapeMoniker Name="VDSelectShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDSelectOptionShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSelectOptionShapeMoniker" ElementName="vDSelectOptionShape" MonikerTypeName="VDSelectOptionShapeMoniker">
        <GeometryShapeMoniker Name="VDSelectOptionShape" />
      </XmlClassData>
      <XmlClassData TypeName="SelectHasOptions" MonikerAttributeName="" SerializeId="true" MonikerElementName="selectHasOptionsMoniker" ElementName="selectHasOptions" MonikerTypeName="SelectHasOptionsMoniker">
        <DomainRelationshipMoniker Name="SelectHasOptions" />
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="MVCVisualDesignerExplorer" />
  <ConnectionBuilders>
    <ConnectionBuilder Name="SeparatorRefersTopWidgetBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="SeparatorRefersTopWidget" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDHoriSeparator" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDWidget" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="SeparatorRefersBottomWidgetBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="SeparatorRefersBottomWidget" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDHoriSeparator" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDWidget" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="SeparatorRefersLeftWidgetBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="SeparatorRefersLeftWidget" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDVertSeparator" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDWidget" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="SeparatorRefersRightWidgetBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="SeparatorRefersRightWidget" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDVertSeparator" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDWidget" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
  </ConnectionBuilders>
  <Diagram Id="1fd11f50-969f-48aa-af5b-31b4277c4671" Description="" Name="VDDiagram" DisplayName="Minimal Language Diagram" Namespace="MVCVisualDesigner" GeneratesDoubleDerived="true">
    <Class>
      <DomainClassMoniker Name="VDView" />
    </Class>
    <ShapeMaps>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDSection" />
        <GeometryShapeMoniker Name="VDSectionShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDWidgetTitle" />
        <ParentElementPath>
          <DomainPath>WidgetHasTitle.Widget/!VDWidget</DomainPath>
        </ParentElementPath>
        <PortMoniker Name="VDWidgetTitlePort" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="VDSectionHead" />
        <ParentElementPath>
          <DomainPath>SectionHasHead.Section/!VDSection</DomainPath>
        </ParentElementPath>
        <GeometryShapeMoniker Name="VDSectionHeadShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="VDSectionBody" />
        <ParentElementPath>
          <DomainPath>SectionHasBody.Section/!VDSection</DomainPath>
        </ParentElementPath>
        <GeometryShapeMoniker Name="VDSectionBodyShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDHoriSeparator" />
        <GeometryShapeMoniker Name="VDHoriSeparatorShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDVertSeparator" />
        <GeometryShapeMoniker Name="VDVertSeparatorShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDForm" />
        <GeometryShapeMoniker Name="VDFormShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDInput" />
        <GeometryShapeMoniker Name="VDInputShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDSubmit" />
        <GeometryShapeMoniker Name="VDSubmitShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDRadio" />
        <GeometryShapeMoniker Name="VDRadioShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDCheckBox" />
        <GeometryShapeMoniker Name="VDCheckBoxShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDSelect" />
        <GeometryShapeMoniker Name="VDSelectShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="VDSelectOption" />
        <ParentElementPath>
          <DomainPath>SelectHasOptions.Select/!VDSelect</DomainPath>
        </ParentElementPath>
        <GeometryShapeMoniker Name="VDSelectOptionShape" />
      </ShapeMap>
    </ShapeMaps>
  </Diagram>
  <Designer CopyPasteGeneration="CopyPasteOnly" FileExtension="amvd" EditorGuid="061c971a-6960-4ef9-9c30-8a8a6543b9f7">
    <RootClass>
      <DomainClassMoniker Name="VDView" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="MVCVisualDesignerSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="Widget">
      <ElementTool Name="SectionTool" ToolboxIcon="Resources\SectionIcon.bmp" Caption="Section" Tooltip="" HelpKeyword="SectionTool">
        <DomainClassMoniker Name="VDSection" />
      </ElementTool>
    </ToolboxTab>
    <ToolboxTab TabText="Form">
      <ElementTool Name="FormTool" ToolboxIcon="Resources\FormIcon.bmp" Caption="Form" Tooltip="" HelpKeyword="FormTool">
        <DomainClassMoniker Name="VDForm" />
      </ElementTool>
      <ElementTool Name="InputTool" ToolboxIcon="Resources\InputIcon.bmp" Caption="Input" Tooltip="" HelpKeyword="InputTool">
        <DomainClassMoniker Name="VDInput" />
      </ElementTool>
      <ElementTool Name="SubmitTool" ToolboxIcon="Resources\ButtonIcon.bmp" Caption="Submit" Tooltip="" HelpKeyword="SubmitTool">
        <DomainClassMoniker Name="VDSubmit" />
      </ElementTool>
      <ElementTool Name="RadioTool" ToolboxIcon="Resources\RadioIcon.bmp" Caption="Radio" Tooltip="" HelpKeyword="RadioTool">
        <DomainClassMoniker Name="VDRadio" />
      </ElementTool>
      <ElementTool Name="CheckBoxTool" ToolboxIcon="Resources\CheckBoxIcon.bmp" Caption="Check Box" Tooltip="" HelpKeyword="CheckBoxTool">
        <DomainClassMoniker Name="VDCheckBox" />
      </ElementTool>
      <ElementTool Name="SelectTool" ToolboxIcon="Resources\SelectIcon.bmp" Caption="Select" Tooltip="" HelpKeyword="SelectTool">
        <DomainClassMoniker Name="VDSelect" />
      </ElementTool>
    </ToolboxTab>
    <Validation UsesMenu="false" UsesOpen="false" UsesSave="false" UsesLoad="false" />
    <DiagramMoniker Name="VDDiagram" />
  </Designer>
  <Explorer ExplorerGuid="a88f64c9-f30b-4d4a-825d-009ceed441ad" Title="MVC Visual Designer Explorer">
    <ExplorerBehaviorMoniker Name="MVCVisualDesigner/MVCVisualDesignerExplorer" />
  </Explorer>
</Dsl>