﻿using System;
using System.Numerics;
using ImGuiNET;
using ImTool;
using ImTool.SDL;

namespace Demo
{
    public class DemoConfig : Configuration
    {

        // add new fields to the config, these will be serialized alongside the default fields
        public int AdditionalIntToBeSaved = 123;
        
        // override the default window settings
        public DemoConfig()
        {
            Title                 = "ImTool Demo";
            GithubRepositoryOwner = "themeldingwars";
            GithubRepositoryName  = "ImTool";
            GithubReleaseName     = "Demo";
        }
    }
    
    public class DemoTool : Tool<DemoTool, DemoConfig>
    {
        protected override bool Initialize(string[] args)
        {
            // override this method if you need to parse cmd-line args or do checks before the tool window is created
            // only the config has been loaded at this point
            // returning false will exit the tool
            
            // example use cases: 
            //      parsing cmd-line messages
            //      mutex check to ensure only a single instance of the tool is running
            //      routing cmd-line messages to a single instance tool
            //
            return true;
        }

        protected override void Load()
        {
            ExtraWidgetsTests.SetupHexView();
            ThemeManager.OnThemeChanged += () => ExtraWidgetsTests.HexViewWidget.SetupSizes();

            // tool window has been created at this point
            // its time to load your tabs now
            window.AddTab(new DemoTab());
            window.AddTab(new DemoTab2());
            config.AdditionalIntToBeSaved = 1234123;
            
            window.AddWindowButton("Test button", () =>
            {
                Console.WriteLine("Test window button clicked :>");
            });
        }

        protected override void Unload()
        {
            // time to clean up your shit
            // you can still do edits to the config,
            // changes will be saved to disk when this method returns
            
        }
    }
    
    public class DemoTab : Tab
    {
        public override string Name { get; } = "ImTool Demo Tab";
        public override ImGuiDockNodeFlags DockSpaceFlags { get; } =  ImGuiDockNodeFlags.PassthruCentralNode;
        
        protected override void CreateDockSpace(Vector2 size)
        {
            // if using PassthruCentralNode dock your workspaces first
            ImGui.DockBuilderDockWindow("Workspace", DockSpaceID);

            // split
            ImGui.DockBuilderSplitNode(DockSpaceID, ImGuiDir.Left, 0.30f, out uint leftId, out uint centerId);
            ImGui.DockBuilderSplitNode(centerId, ImGuiDir.Down, 0.20f, out uint centerBottomId, out uint centerTopId);
            ImGui.DockBuilderSplitNode(centerTopId, ImGuiDir.Right, 0.40f, out uint centerRightId, out uint centerLeftId);
            ImGui.DockBuilderSplitNode(centerRightId, ImGuiDir.Down, 0.40f, out uint centerRightBottomId, out uint centerRightTopId);

            // assign
            //ImGui.DockBuilderDockWindow("Hex View", topLeftId);
            ImGui.DockBuilderDockWindow("Test Log Window", centerBottomId);
            ImGui.DockBuilderDockWindow("Dear ImGui Demo", leftId);
            ImGui.DockBuilderDockWindow("Dear ImGui Metrics/Debugger", centerRightTopId);
            ImGui.DockBuilderDockWindow("Extensions test :>", centerRightBottomId);
            
        }
        
        public override void SubmitContent()
        {
            ImGuiWindowFlags workspaceWindowFlags =  ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoNav | ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoTitleBar;
            ImGui.Begin("Workspace", workspaceWindowFlags);
            ImGui.End();
            
            ImGui.ShowDemoWindow();
            ImGui.ShowMetricsWindow();
            ExtraWidgetsTests.Draw();
        }
        public override void SubmitSettings(bool active)
        {
            ImGui.Text($"Submitted from DemoTab.SubmitSettings({active})");
        }
        public override void SubmitMainMenu()
        {
            if (ImGui.BeginMenu("File"))
            {
                ImGui.EndMenu();
            }
            
            if (ImGui.BeginMenu("Edit"))
            {
                ImGui.EndMenu();
            }
            
            if (ImGui.BeginMenu("View"))
            {
                ImGui.EndMenu();
            }
            
            if (ImGui.BeginMenu("Navigate"))
            {
                ImGui.EndMenu();
            }
            if (ImGui.BeginMenu("Help"))
            {
                ImGui.EndMenu();
            }
        }
        
    }
    
    public class DemoTab2 : Tab
    {
        public override string Name { get; } = "ImTool Demo Tab 2";
        public override void SubmitContent()
        {
            ImGui.Begin("Winduu");
            ImGui.ShowStyleEditor();
            ImGui.End();
            
        }

        public override void SubmitSettings(bool active)
        {
            ImGui.Text($"Submitted from DemoTab2.SubmitSettings({active})");
        }


        public override void SubmitMainMenu()
        {
            if (ImGui.BeginMenu("Fileuu"))
            {
                ImGui.EndMenu();
            }
            
            if (ImGui.BeginMenu("Edituu"))
            {
                ImGui.EndMenu();
            }
            
            if (ImGui.BeginMenu("Viewuu"))
            {
                ImGui.EndMenu();
            }
            
            if (ImGui.BeginMenu("Navigateuu"))
            {
                ImGui.EndMenu();
            }
            if (ImGui.BeginMenu("Helpuu"))
            {
                ImGui.EndMenu();
            }
        }
        
    }
}