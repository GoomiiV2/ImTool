﻿using System;
using System.IO;
using System.Numerics;
using Newtonsoft.Json;
using Veldrid;

namespace ImTool
{
    public class Configuration
    {
        [JsonIgnore] public string File;
        [JsonIgnore] public string ToolDataPath;
        
        public string Title = "ImTool";
        public string Theme = "CorporateGrey";
        public string GithubRepositoryOwner;
        public string GithubRepositoryName;
        public string GithubReleaseName;
        public bool GithubGetPrereleases;
        public WindowState WindowState = WindowState.Normal;
        public WindowState PreviousWindowState = WindowState.Maximized;
        public int Monitor = 0;
        public Vector2 MinimumWindowSize = new Vector2(800, 600);
        public Rect NormalWindowBounds = new Rect(50, 50, 1280, 720);
        public int BorderSize = 1;
        public bool VSync = false;
        public int FpsLimit = 144;
        public GraphicsBackend GraphicsBackend = GraphicsBackend.OpenGL;
        public bool PowerSaving = true;

        public static T Load<T>(string toolDataPath = "") where T : Configuration
        {
            string configPath = Path.Combine(toolDataPath, "Config");
            Directory.CreateDirectory(configPath);
            
            string name = typeof(T).FullName;
            string file = Path.Join(configPath, name + ".json");
            
            T instance = (T) Serializer<T>.DeserializeFromFile(file);
            if (instance == null)
            {
                instance = (T) Activator.CreateInstance(typeof(T));
            }
            
            instance.File = file;
            instance.ToolDataPath = toolDataPath;
            return instance;
        }
    }

    public static class ConfigurationExtensions
    {
        public static void Save<T>(this T instance) where T : Configuration
        {
            Serializer<T>.SerializeToFile(instance, instance.File);
        }
    }
}