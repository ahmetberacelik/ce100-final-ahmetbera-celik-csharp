/**
* @file Hometracker.cs
* @brief Contains the definition of the Hometracker class.
*/
using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
/** 
 * @brief Contains classes that define the Hometracker system's functionalities.
 */
namespace HometrackerLibrary
{
    public class UtilityUsage
    {
        public string Username { get; set; }
        public double Electricity { get; set; }
        public double Water { get; set; }
        public double Gas { get; set; }

        public UtilityUsage() { }

        public UtilityUsage(string username, double electricity, double water, double gas)
        {
            Username = username;
            Electricity = electricity;
            Water = water;
            Gas = gas;
        }
    }
    public class Reminder
    {
        public string Username { get; set; }
        public string ReminderText { get; set; }
        public int DaysAfter { get; set; }

        public Reminder(string username, string reminderText, int daysAfter)
        {
            Username = username;
            ReminderText = reminderText;
            DaysAfter = daysAfter;
        }
    }
    /**
     * @brief Represents a user in the Hometracker system.
     *
     * This class stores user information such as username and password.
     */
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }

        // Constructor
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
    public class Node
    {
        public string Username { get; set; }
        public double Electricity { get; set; }
        public double Water { get; set; }
        public double Gas { get; set; }
        public List<Node> Neighbors { get; set; }
        public Dictionary<Node, int> Capacities { get; set; }
        public Dictionary<Node, int> ResidualCapacities { get; set; }

        public Node(string username)
        {
            Username = username;
            Neighbors = new List<Node>();
            Capacities = new Dictionary<Node, int>();
            ResidualCapacities = new Dictionary<Node, int>();
        }

        public int ResidualCapacityTo(Node neighbor)
        {
            if (ResidualCapacities.ContainsKey(neighbor))
                return ResidualCapacities[neighbor]; return 0; }
        public void UpdateResidualCapacityTo(Node neighbor, int capacityChange)
        {
            if (ResidualCapacities.ContainsKey(neighbor))
            {
                ResidualCapacities[neighbor] += capacityChange;
            }
            else
            {
                ResidualCapacities[neighbor] = capacityChange;
            }
        }

        public void AddNeighbor(Node neighbor, int capacity)
        {
            Neighbors.Add(neighbor);
            Capacities[neighbor] = capacity;
            ResidualCapacities[neighbor] = capacity; // Initially, residual capacity is equal to the original capacity
        }
    }
    public class Edge
    {
        public Node U { get; set; }
        public Node V { get; set; }
        public int Weight { get; set; } }
    /**
         * @brief Represents the Hometracker System.
         * 
         * The Hometracker class serves as the main class for the Hometracker system.
         * It encapsulates various functionalities related to catalog search, reservations, events, and library information.
         */
    public class Hometracker
    {
        public static bool guestMode = false;

        public static string active_user;

        public int[,] residualGraph;
        public int nodeCount;
        public bool IsTestMode { get; set; } = false;
        /**
        * @brief Reads a line from the console input.
        */
        public void take_enter_input()
        {
            Console.ReadLine();
        }
        /**
        * @brief Clears the console screen if the application is not in test mode.
        */
        public void ClearScreen()
        {
            if (!IsTestMode)
            {
                Console.Clear();
            }
        }
        public bool MainMenu(bool authenticationResult)
        {
            if (!authenticationResult)
            {
                return false;
            }
            while (true)
            {
                ClearScreen();
                Console.WriteLine("+-------------------------------------+");
                Console.WriteLine("|            MAIN MENU                |");
                Console.WriteLine("+-------------------------------------+");
                Console.WriteLine("| 1. Utility Logging                  |");
                Console.WriteLine("| 2. Expense Calculation              |");
                Console.WriteLine("| 3. Trend Analysis                   |");
                Console.WriteLine("| 4. Reminder Setup                   |");
                Console.WriteLine("| 5. Exit                             |");
                Console.WriteLine("+-------------------------------------+");
                Console.Write("Please select an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input, please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        UtilityLogging(guestMode);
                        break;
                    case 2:
                        CalculateAndShowExpenses(guestMode);
                        break;
                    case 3:
                        ShowTrendAnalysis();
                        break;
                    case 4:
                        ReminderSetup(guestMode);
                        break;
                    case 5:
                        Console.WriteLine("Exiting program...Press enter!");
                        Console.ReadLine();
                        return true;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        public int SaveUser(User user, string filename)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Append, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(user.username);
                writer.Write(user.password);
            }
            return 1;
        }
        public int AuthenticateUser(string username, string password, string filename)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                while (stream.Position < stream.Length)
                {
                    string fileUsername = reader.ReadString();
                    string filePassword = reader.ReadString();
                    if (fileUsername == username && filePassword == password)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }
        public bool UserAuthentication()
        {
            ClearScreen();
            string filename = "Users.bin";
            SaveUser(new User("Ahmet Bera Celik", "qwerty"), filename);
            SaveUser(new User("Enes Koy", "123456"), filename);
            SaveUser(new User("Ugur Coruh", "asdasd"), filename);

            while (true)
            {
                ClearScreen();
                Console.WriteLine("+---------------------------+");
                Console.WriteLine("|  LOGIN AND REGISTER MENU  |");
                Console.WriteLine("+---------------------------+");
                Console.WriteLine("| 1. Login                  |");
                Console.WriteLine("| 2. Register               |");
                Console.WriteLine("| 3. Guest Mode             |");
                Console.WriteLine("| 4. Exit Program           |");
                Console.WriteLine("+---------------------------+");
                Console.Write("Please select an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input, please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Please enter your username: ");
                        string tempUsername = Console.ReadLine();
                        Console.Write("Please enter your password: ");
                        string tempPassword = Console.ReadLine();

                        if (AuthenticateUser(tempUsername, tempPassword, filename) == 1)
                        {
                            active_user = tempUsername;
                            ClearScreen();
                            Console.WriteLine($"Welcome {tempUsername}");
                            take_enter_input();
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("You entered wrong username or password. Please try again.");
                        }
                        break;

                    case 2:
                        Console.Write("Please enter your username: ");
                        tempUsername = Console.ReadLine();
                        Console.Write("Please enter your password: ");
                        tempPassword = Console.ReadLine();

                        SaveUser(new User(tempUsername, tempPassword), filename);
                        active_user = tempUsername;
                        ClearScreen();
                        Console.WriteLine($"User registered successfully.\nWelcome {tempUsername}");
                        take_enter_input();
                        return true;

                    case 3:
                        guestMode = true;
                        return true;

                    case 4:
                        Console.WriteLine("Exiting program...Press enter!");
                        take_enter_input();
                        return false;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        take_enter_input();
                        break;
                }
            }
        }
        public void BFS(Node startNode)
        {
            Queue<Node> queue = new Queue<Node>();
            HashSet<string> visited = new HashSet<string>();

            queue.Enqueue(startNode);
            visited.Add(startNode.Username);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                Console.WriteLine($"Username: {node.Username}, Electricity: {node.Electricity}, Water: {node.Water}, Gas: {node.Gas}");

                foreach (var neighbor in node.Neighbors)
                {
                    if (!visited.Contains(neighbor.Username))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor.Username);
                    }
                }
            }
        }
        public void DFS(Node startNode)
        {
            Stack<Node> stack = new Stack<Node>();
            HashSet<string> visited = new HashSet<string>();

            stack.Push(startNode);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (!visited.Contains(node.Username))
                {
                    Console.WriteLine($"Username: {node.Username}, Electricity: {node.Electricity}, Water: {node.Water}, Gas: {node.Gas}");
                    visited.Add(node.Username);

                    foreach (var neighbor in node.Neighbors)
                    {
                        if (!visited.Contains(neighbor.Username))
                        {
                            stack.Push(neighbor);
                        }
                    }
                }
            }
        }
        public bool LoadGraph(List<UtilityUsage> usages, List<Node> nodes)
        {
            var nodeDict = new Dictionary<string, Node>();

            foreach (var usage in usages)
            {
                if (!nodeDict.ContainsKey(usage.Username))
                    nodeDict[usage.Username] = new Node(usage.Username)
                    {
                        Electricity = usage.Electricity,
                        Water = usage.Water,
                        Gas = usage.Gas
                    };
            }

            foreach (var usage in usages)
            {
                if (usage.Username == active_user)
                {
                    foreach (var otherUsage in usages)
                    {
                        if (otherUsage.Username != usage.Username)
                        {
                            Node firstUserNode = nodeDict[usage.Username];
                            Node secondUserNode = nodeDict[otherUsage.Username];

                            int capacity = (int)(firstUserNode.Electricity + secondUserNode.Electricity);
                            firstUserNode.AddNeighbor(secondUserNode, capacity);
                            secondUserNode.AddNeighbor(firstUserNode, capacity);
                        }
                    }
                }
            }

            nodes.AddRange(nodeDict.Values);
            return true;
        }

        public void ViewUtilityUsages(Node rootNode)
        {
            Console.WriteLine("Select search method:");
            Console.WriteLine("1. BFS");
            Console.WriteLine("2. DFS");

            int searchType;
            if (!int.TryParse(Console.ReadLine(), out searchType))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            switch (searchType)
            {
                case 1:
                    Console.WriteLine($"Utility usages (BFS) for {rootNode.Username}:");
                    BFS(rootNode);
                    break;
                case 2:
                    Console.WriteLine($"Utility usages (DFS) for {rootNode.Username}:");
                    DFS(rootNode);
                    break;
                default:
                    Console.WriteLine("Invalid Search Type.");
                    break;
            }
        }

        public bool FindPath(Node source, Node sink, Dictionary<Node, Node> parentMap)
        {
            if (source == null || sink == null) { Console.WriteLine("There is no path."); return false; }
            Queue<Node> queue = new Queue<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            queue.Enqueue(source);
            visited.Add(source);
            parentMap[source] = null;

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                foreach (var neighbor in currentNode.Neighbors)
                {
                    if (!visited.Contains(neighbor) && currentNode.ResidualCapacityTo(neighbor) > 0)
                    {
                        parentMap[neighbor] = currentNode;
                        if (neighbor == sink)
                            return true;

                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }

            return false;
        }

        public int FordFulkerson(Node source, Node sink)
        {
            int maxFlow = 0;
            Dictionary<Node, Node> parentMap = new Dictionary<Node, Node>();

            while (FindPath(source, sink, parentMap))
            {
                int pathFlow = int.MaxValue;

                // Calculate flow
                Node currentNode = sink;
                while (currentNode != source)
                {
                    Node parent = parentMap[currentNode];
                    pathFlow = Math.Min(pathFlow, parent.ResidualCapacityTo(currentNode)); // Assuming this method checks capacity
                    currentNode = parent;
                }

                // Update capacities
                currentNode = sink;
                while (currentNode != source)
                {
                    Node parent = parentMap[currentNode];
                    parent.UpdateResidualCapacityTo(currentNode, -pathFlow); // Assuming this updates the capacities
                    currentNode = parent;
                }

                maxFlow += pathFlow;
            }

            return maxFlow;
        }
        public bool SaveUtilityUsage(UtilityUsage usage, string filename)
        {
            List<UtilityUsage> usages = LoadUtilityUsages(filename);
            bool found = false;
            for (int i = 0; i < usages.Count; i++)
            {
                if (usages[i].Username == usage.Username)
                {
                    usages[i].Electricity = usage.Electricity;
                    usages[i].Water = usage.Water;
                    usages[i].Gas = usage.Gas;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                usages.Add(usage);
            }

            try
            {
                using (FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    foreach (var item in usages)
                    {
                        writer.Write(item.Username);
                        writer.Write(item.Electricity);
                        writer.Write(item.Water);
                        writer.Write(item.Gas);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<UtilityUsage> LoadUtilityUsages(string filename)
        {
            List<UtilityUsage> usages = new List<UtilityUsage>();
            try
            {
                using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    while (stream.Position < stream.Length)
                    {
                        string username = reader.ReadString();
                        double electricity = reader.ReadDouble();
                        double water = reader.ReadDouble();
                        double gas = reader.ReadDouble();
                        usages.Add(new UtilityUsage(username, electricity, water, gas));
                    }
                }
            }
            catch
            {
                // Handle exceptions or log errors as necessary
            }
            return usages;
        }
        public int EdmondsKarp(Node source, Node sink)
        {
            int maxFlow = 0;
            Dictionary<Node, Node> parentMap = new Dictionary<Node, Node>();

            while (FindPath(source, sink, parentMap)) // This uses BFS to find path
            {
                int pathFlow = int.MaxValue;

                for (Node v = sink; v != source; v = parentMap[v])
                {
                    Node u = parentMap[v];
                    pathFlow = Math.Min(pathFlow, u.ResidualCapacityTo(v));
                }

                for (Node v = sink; v != source; v = parentMap[v])
                {
                    Node u = parentMap[v];
                    u.UpdateResidualCapacityTo(v, -pathFlow);
                    v.UpdateResidualCapacityTo(u, pathFlow);
                }

                maxFlow += pathFlow;
            }

            return maxFlow;
        }
        public void CalculateAndShowMaximumFlow(Node source, Node sink)
        {
            Console.WriteLine("1. Ford-Fulkerson");
            Console.WriteLine("2. Edmonds-Karp");
            Console.WriteLine("Choose an algorithm: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            int flow = 0;
            switch (choice)
            {
                case 1:
                    flow = FordFulkerson(source, sink);
                    break;
                case 2:
                    flow = EdmondsKarp(source, sink);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }

            Console.WriteLine($"Maximum flow: {flow}");
        }

        public void Dijkstra(Node source, Dictionary<Node, int> dist, Dictionary<Node, Node> prev)
        {
            var nodes = new List<Node>();
            foreach (var node in nodes) { dist[node] = int.MaxValue; prev[node] = null; }
            dist[source] = 0;

            var priorityQueue = new PriorityQueue<Node, int>();
            priorityQueue.Enqueue(source, 0);

            while (priorityQueue.Count != 0)
            {
                var u = priorityQueue.Dequeue();

                foreach (var neighbor in u.Neighbors)
                {
                    int alt = dist[u] + u.Capacities[neighbor];
                    if (alt < dist[neighbor])
                    {
                        dist[neighbor] = alt;
                        prev[neighbor] = u;
                        priorityQueue.Enqueue(neighbor, alt);
                    }
                }
            }
        }
        public bool BellmanFord(Node source, Dictionary<Node, int> dist)
        {
            var nodes = new List<Node>(); // This needs to be all the nodes in the graph
            foreach (var node in nodes) { dist[node] = int.MaxValue; }
            dist[source] = 0;

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                foreach (var u in nodes)
                {
                    foreach (var v in u.Neighbors)
                    {
                        if (dist[u] != int.MaxValue && dist[u] + u.Capacities[v] < dist[v])
                        {
                            dist[v] = dist[u] + u.Capacities[v];
                        }
                    }
                }
            }

            foreach (var u in nodes)
            {
                foreach (var v in u.Neighbors)
                {
                    if (dist[u] != int.MaxValue && dist[u] + u.Capacities[v] < dist[v])
                    {
                        return false;
                    }
                }
            }
            return true; }
        public void InitializeCosts(Dictionary<Node, Dictionary<Node, int>> costs, List<Node> nodes)
        {
            foreach (var u in nodes)
            {
                costs[u] = new Dictionary<Node, int>();
                foreach (var v in nodes)
                {
                    costs[u][v] = (u == v) ? 0 : int.MaxValue; // Set diagonal to 0, others to infinity
                }
            }
        }
        public void PrimMST(List<Node> nodes)
        {
            var key = new Dictionary<Node, int>();
            var mstSet = new Dictionary<Node, bool>();
            var parent = new Dictionary<Node, Node>();

            foreach (var node in nodes)
            {
                key[node] = int.MaxValue;
                mstSet[node] = false;
                parent[node] = null;
            }

            key[nodes[0]] = 0; // Start with the first node

            for (int count = 0; count < nodes.Count - 1; count++)
            {
                var u = GetMinKeyNode(key, mstSet);

                // Check if u is null to avoid ArgumentNullException
                if (u == null)
                {
                    Console.WriteLine("No valid next node was found. Exiting Prim's MST algorithm.");
                    return; // Exit the method if no valid node is found
                }

                mstSet[u] = true;

                foreach (var neighbor in u.Neighbors)
                {
                    if (!mstSet[neighbor] && u.Capacities[neighbor] < key[neighbor])
                    {
                        parent[neighbor] = u;
                        key[neighbor] = u.Capacities[neighbor];
                    }
                }
            }
        }

        private Node GetMinKeyNode(Dictionary<Node, int> key, Dictionary<Node, bool> mstSet)
        {
            int min = int.MaxValue;
            Node minNode = null;

            foreach (var node in key.Keys)
            {
                if (!mstSet[node] && key[node] < min)
                {
                    min = key[node];
                    minNode = node;
                }
            }

            return minNode; // Can return null if all nodes are included in mstSet
        }

        public Node Find(Dictionary<Node, Node> parent, Node i)
        {
            if (parent[i] == i)
                return i;
            return parent[i] = Find(parent, parent[i]);
        }
        public void Union(Dictionary<Node, Node> parent, Dictionary<Node, int> rank, Node i, Node j)
        {
            Node rootI = Find(parent, i);
            Node rootJ = Find(parent, j);

            if (rootI != rootJ)
            {
                if (rank[rootI] < rank[rootJ])
                    parent[rootI] = rootJ;
                else if (rank[rootI] > rank[rootJ])
                    parent[rootJ] = rootI;
                else
                {
                    parent[rootJ] = rootI;
                    rank[rootI]++;
                }
            }
        }
        public void KruskalMST(List<Node> nodes, List<Edge> edges)
        {
            var parent = new Dictionary<Node, Node>();
            var rank = new Dictionary<Node, int>();

            foreach (var node in nodes)
            {
                parent[node] = node;
                rank[node] = 0;
            }

            edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));

            Console.WriteLine("Kruskal's MST:");
            foreach (var edge in edges) { Node u = Find(parent, edge.U); Node v = Find(parent, edge.V); if (u != v) { Console.WriteLine($"{edge.U.Username} - {edge.V.Username}: {edge.Weight}"); Union(parent, rank, u, v); } }
        }
        public bool UtilityLogging(bool localGuestMode)
        {
            if (localGuestMode)
            {
                Console.WriteLine("Guest mode does not have permission to log utility.");
                take_enter_input();
                return false;
            }

            ClearScreen();
            string filename = "utility_usages.bin";
            UtilityUsage usage = new UtilityUsage { Username = active_user };
            List<Node> nodes = new List<Node>();
            List<UtilityUsage> usages = LoadUtilityUsages(filename);

            Dictionary<Node, Dictionary<Node, int>> costs;

            while (true)
            {
                ClearScreen();
                Console.WriteLine("+-------------------------------------+");
                Console.WriteLine("|          UTILITY LOGGING MENU       |");
                Console.WriteLine("+-------------------------------------+");
                Console.WriteLine("| 1. Log Electricity                  |");
                Console.WriteLine("| 2. Log Water                        |");
                Console.WriteLine("| 3. Log Gas                          |");
                Console.WriteLine("| 4. View Total Usages                |");
                Console.WriteLine("| 5. Calculate and Show Maximum Flow  |");
                Console.WriteLine("| 6. Shortest paths from Houses       |");
                Console.WriteLine("|        (For Information)            |");
                Console.WriteLine("| 7. Minimum Spanning Tree            |");
                Console.WriteLine("| 8. Return to Main Menu              |");
                Console.WriteLine("+-------------------------------------+");
                Console.Write("Please select an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input, please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ClearScreen();
                        Console.Write("Enter electricity usage (kWh): ");
                        usage.Electricity = double.Parse(Console.ReadLine());
                        SaveUtilityUsage(usage, filename);
                        usages = LoadUtilityUsages(filename);
                        LoadGraph(usages, nodes);
                        break;

                    case 2:
                        ClearScreen();
                        Console.Write("Enter water usage (cubic meters): ");
                        usage.Water = double.Parse(Console.ReadLine());
                        SaveUtilityUsage(usage, filename);
                        usages = LoadUtilityUsages(filename);
                        LoadGraph(usages, nodes);
                        break;

                    case 3:
                        ClearScreen();
                        Console.Write("Enter gas usage (cubic meters): ");
                        usage.Gas = double.Parse(Console.ReadLine());
                        SaveUtilityUsage(usage, filename);
                        usages = LoadUtilityUsages(filename);
                        LoadGraph(usages, nodes);
                        break;

                    case 4:
                        ClearScreen();
                        LoadGraph(usages, nodes);
                        Node rootNode = nodes.Find(n => n.Username == active_user);
                        if (rootNode == null)
                        {
                            rootNode = new Node(active_user);
                            nodes.Add(rootNode);
                        }
                        ViewUtilityUsages(rootNode);
                        take_enter_input();
                        break;

                    case 5:
                        ClearScreen();
                        Node source = nodes.Find(n => n.Username == active_user);
                        Node sink = new Node("System Sink"); // Define your sink node appropriately
                        CalculateAndShowMaximumFlow(source, sink);
                        Console.WriteLine("Press enter to continue...");
                        take_enter_input();
                        break;;

                    case 6:
                        ClearScreen();
                        costs = new Dictionary<Node, Dictionary<Node, int>>();
                        List<Node> localNodes = new List<Node>();

                        // Nodes are added here
                        localNodes.Add(new Node("House1"));
                        localNodes.Add(new Node("House2"));
                        localNodes.Add(new Node("House3"));

                        // Initialize costs
                        InitializeCosts(costs, localNodes);

                        // Define the connections if there are at least three nodes
                        if (localNodes.Count >= 3)
                        {
                            costs[localNodes[0]][localNodes[1]] = 10;  // Connection from House1 to House2
                            costs[localNodes[1]][localNodes[2]] = 15;  // Connection from House2 to House3
                            costs[localNodes[0]][localNodes[2]] = 20;  // Connection from House1 to House3
                            costs[localNodes[2]][localNodes[0]] = 20;  // Connection from House3 to House1
                        }

                        Dictionary<Node, int> distances = new Dictionary<Node, int>();
                        Dictionary<Node, Node> predecessors = new Dictionary<Node, Node>();

                        // Execute Dijkstra's algorithm
                        Dijkstra(localNodes[0], distances, predecessors);
                        Console.WriteLine("Shortest paths from House1 using Dijkstra's algorithm:");
                        foreach (var node in localNodes)
                        {
                            if (distances.ContainsKey(node))
                            {
                                Console.WriteLine($"From House1 to {node.Username}: {distances[node]}");
                            }
                            else
                            {
                                Console.WriteLine($"Distance information for {node.Username} is not available.");
                            }
                        }


                        // Execute Bellman-Ford algorithm
                        BellmanFord(localNodes[0], distances);
                        Console.WriteLine("Shortest paths from House1 using Bellman-Ford algorithm:");
                        foreach (var node in localNodes)
                        {
                            if (distances.ContainsKey(node))
                            {
                                Console.WriteLine($"From House1 to {node.Username}: {distances[node]}");
                            }
                            else
                            {
                                Console.WriteLine($"Distance information for {node.Username} is not available.");
                            }
                        }

                        Console.WriteLine("Press enter to continue...");
                        take_enter_input();
                        break;

                    case 7:
                        ClearScreen();
                        nodes.Add(new Node("House1"));
                        nodes.Add(new Node("House2"));
                        nodes.Add(new Node("House3"));

                        costs = new Dictionary<Node, Dictionary<Node, int>>();
                        InitializeCosts(costs, nodes);

                        if (nodes.Count >= 3)
                        {
                            costs[nodes[0]][nodes[1]] = 10;
                            costs[nodes[1]][nodes[2]] = 15;
                            costs[nodes[0]][nodes[2]] = 20;
                            costs[nodes[2]][nodes[0]] = 20;
                        }

                        PrimMST(nodes);
                        KruskalMST(nodes, new List<Edge>());
                        Console.WriteLine("Press enter to continue...");
                        take_enter_input();
                        break;
                    case 8:
                        return true;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        take_enter_input();
                        break;
                }
            }
        }
        public bool CalculateAndShowExpenses(bool localGuestMode)
        {
            ClearScreen();
            if (localGuestMode)
            {
                Console.WriteLine("Guest mode does not have permission to access expense calculation.");
                take_enter_input();
                return false;
            }

            string filename = "utility_usages.bin";
            List<UtilityUsage> usages = LoadUtilityUsages(filename);

            if (usages.Count == 0)
            {
                Console.WriteLine("No utility data found.");
                take_enter_input();
                return false;
            }

            bool found = false;
            foreach (var usage in usages)
            {
                if (usage.Username == active_user)
                {
                    found = true;
                    double electricityCost = usage.Electricity * 0.145; // ELECTRICITY_PRICE_PER_KWH
                    double waterCost = usage.Water * 1.30;             // WATER_PRICE_PER_CUBIC_METER
                    double gasCost = usage.Gas * 0.75;                 // GAS_PRICE_PER_CUBIC_METER
                    double totalCost = electricityCost + waterCost + gasCost;

                    Console.WriteLine($"Electricity cost: ${electricityCost}");
                    Console.WriteLine($"Water cost: ${waterCost}");
                    Console.WriteLine($"Gas cost: ${gasCost}");
                    Console.WriteLine($"Total utility cost: ${totalCost}");
                    take_enter_input();
                    return true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"No utility data found for the user: {active_user}.");
                take_enter_input();
                return false;
            }

            return true;
        }

        public void ShowTrendAnalysis()
        {
            ClearScreen();
            const int totalElectricityUsage = 546515;
            const int totalWaterUsage = 3193746;
            const int totalGasUsage = 2843973;

            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("|           Trend Analysis            |");
            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine($"Countrywide electricity usage: {totalElectricityUsage} kWh");
            Console.WriteLine($"Countrywide water usage: {totalWaterUsage} cubic meters");
            Console.WriteLine($"Countrywide gas usage: {totalGasUsage} cubic meters");
            Console.WriteLine("+-------------------------------------+");
            take_enter_input();
        }

        public int SaveReminder(Reminder reminder, string filename)
        {

            using (FileStream stream = new FileStream(filename, FileMode.Append, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                Console.WriteLine("Attempting to write reminder to file...");
                writer.Write(reminder.Username ?? string.Empty);
                writer.Write(reminder.ReminderText ?? string.Empty);
                writer.Write(reminder.DaysAfter);
                stream.Flush();
                Console.WriteLine("Reminder written to file successfully.");
            }
            return 1;
        }


        public int LoadReminders(string username, string filename, List<Reminder> reminders, int maxReminders)
        {
            try
            {
                using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    while (stream.Position < stream.Length && reminders.Count < maxReminders)
                    {
                        string reminderUsername = reader.ReadString();
                        string reminderText = reader.ReadString();
                        int daysAfter = reader.ReadInt32();

                        if (reminderUsername == username)
                        {
                            reminders.Add(new Reminder(reminderUsername, reminderText, daysAfter));
                        }
                    }
                }
                return reminders.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading reminders: {ex.Message}");
                return 0;
            }
        }


        public bool PrintReminders(List<Reminder> reminders)
        {
            if (reminders.Count == 0)
            {
                Console.WriteLine("You have no reminders.");
                take_enter_input();
                return false;
            }

            Console.WriteLine("+------------------------------------+");
            Console.WriteLine("|        Upcoming Reminders          |");
            Console.WriteLine("+------------------------------------+");
            for (int i = 0; i < reminders.Count; i++)
            {
                Console.WriteLine($"Reminder {i + 1}:");
                Console.WriteLine($"Username: {reminders[i].Username}");
                Console.WriteLine($"Reminder Text: {reminders[i].ReminderText}");
                Console.WriteLine($"Days After: {reminders[i].DaysAfter}");
                Console.WriteLine("--------------------------------------");
            }
            take_enter_input();
            return true;
        }

        public bool ReminderSetup(bool localGuestMode)
        {
            if (localGuestMode)
            {
                Console.WriteLine("Guest mode does not have permission to set reminders.");
                take_enter_input();
                return false;
            }

            string filename = "Reminders.bin";
            List<Reminder> reminders = new List<Reminder>();
            int reminderCount = 0;

            while (true)
            {
                ClearScreen();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|       REMINDER SETUP MENU       |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("| 1. Set Payment Reminders        |");
                Console.WriteLine("| 2. View Upcoming Reminders      |");
                Console.WriteLine("| 3. Back to Main Menu            |");
                Console.WriteLine("+---------------------------------+");
                Console.Write("Please select an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input, please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter reminder text: ");
                        string reminderText = Console.ReadLine();

                        Console.Write("Enter number of days after which to remind: ");
                        if (!int.TryParse(Console.ReadLine(), out int daysAfter))
                        {
                            Console.WriteLine("Invalid input for days, please enter a number.");
                            take_enter_input();
                            break;
                        }

                        Reminder reminder = new Reminder(active_user, reminderText, daysAfter);

                        if (SaveReminder(reminder, filename) == 1)
                        {
                            Console.WriteLine("Reminder saved successfully.");
                            reminderCount++;
                        }
                        take_enter_input();
                        break;

                    case 2:
                        reminders.Clear();
                        reminderCount = LoadReminders(active_user, filename, reminders, 100);
                        PrintReminders(reminders);
                        break;

                    case 3:
                        return true;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        take_enter_input();
                        break;
                }
            }
        }
    }
}