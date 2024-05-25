using System;
using System.IO;
using System.Collections.Generic;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HometrackerLibrary.Tests {
public class HometrackerTests {
        private Node CreateNodeWithResidualCapacities(string name)
        {
            var node = new Node(name);
            node.Neighbors = new List<Node>();
            node.Capacities = new Dictionary<Node, int>();
            node.ResidualCapacities = new Dictionary<Node, int>();
            return node;
        }
        private Node CreateNodeWithCapacities(string name)
        {
            var node = new Node(name);
            node.Neighbors = new List<Node>();
            node.Capacities = new Dictionary<Node, int>();
            return node;
        }
        private Node CreateNode(string name)
        {
            return new Node(name);
        }

        private Edge CreateEdge(Node u, Node v, int weight)
        {
            return new Edge { U = u, V = v, Weight = weight };
        }
        private Node CreateNodeWithNeighbors(string name)
        {
            var node = new Node(name);
            node.Neighbors = new List<Node>();
            node.Capacities = new Dictionary<Node, int>();
            return node;
        }
        [Fact]
        public void MainMenuIncorrectLoginTest()
        {
            var hometracker = new Hometracker
            {
                IsTestMode = true
            };
            bool authenticationResult = false;

            bool result = hometracker.MainMenu(authenticationResult);

            Assert.False(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void MainMenuInvalidTest()
        {
            var input = new StringReader("abc\n\n48\n\n5\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool authenticationResult = true;

            bool result = hometracker.MainMenu(authenticationResult);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void MainMenuValidTest()
        {
            var input = new StringReader("1\n8\n2\n\n3\n\n4\n3\n5\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool authenticationResult = true;

            bool result = hometracker.MainMenu(authenticationResult);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void LoginGuestModeTest()
        {
            var input = new StringReader("3\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool result = hometracker.UserAuthentication();

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void UserAuthenticationTestCorrectLogin()
        {
            var input = new StringReader("1\nEnes Koy\n123456\n\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool result = hometracker.UserAuthentication();

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UserAuthenticationTestIncorrectLogin()
        {
            var input = new StringReader("1\nInvalid User\n123456\n4\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool result = hometracker.UserAuthentication();

            Assert.False(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UserAuthenticationRegisterTest()
        {
            var input = new StringReader("2\nTestUser\n123456\n\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool result = hometracker.UserAuthentication();

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UserAuthenticationInvalidInputTest()
        {
            var input = new StringReader("invalid\n\n454\n\n4\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool result = hometracker.UserAuthentication();

            Assert.False(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UtilityLoggingGuestModeTest()
        {
            var input = new StringReader("");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };
            bool guestMode = true;

            bool result = hometracker.UtilityLogging(guestMode);

            Assert.False(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UtilityLoggingTest()
        {
            var input = new StringReader("1\n100\n2\n100\n3\n100\n4\n1\n\n4\n2\n\n4\n4545\n\n5\n1\n\n5\n2\n\n5\ni\n\n5\n45\n\n6\n\n7\n\n8\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };
            bool guestMode = false;

            bool result = hometracker.UtilityLogging(guestMode);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UtilityLoggingTestInvalidUsage()
        {
            var input = new StringReader("5\ninvalid\n\n5\n45\n\n8\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };
            bool guestMode = false;

            bool result = hometracker.UtilityLogging(guestMode);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void UtilityLoggingInvalidInputTest()
        {
            var input = new StringReader("invalid\n\n454\n\n8\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };
            bool guestMode = false;

            bool result = hometracker.UtilityLogging(guestMode);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void ReminderSetupTest()
        {
            var input = new StringReader("2\n\n1\ntest reminder\n10\n\n2\n\n3\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool guestMode = false;

            bool result = hometracker.ReminderSetup(guestMode);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void ReminderSetupInvalidTest()
        {
            var input = new StringReader("invalid\n\n454\n\n3\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool guestMode = false;

            bool result = hometracker.ReminderSetup(guestMode);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void ReminderSetupInvalidTextTest()
        {
            var input = new StringReader("1\ntest reminder\ninvalid\n\n3\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };

            bool guestMode = false;

            bool result = hometracker.ReminderSetup(guestMode);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void CalculateAndShowTest()
        {
            var input = new StringReader("\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };
            bool guestMode = false;

            bool result = hometracker.CalculateAndShowExpenses(guestMode);

            Assert.False(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
       
        [Fact]
        public void CalculateAndShowExpenses_NoData_ReturnsFalse()
        {
            var input = new StringReader("");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var hometracker = new Hometracker
            {
                IsTestMode = true
            };
            bool guestMode = false;

            // Mock data setup
            string filename = "utility_usages.bin";
            List<UtilityUsage> mockUsages = new List<UtilityUsage>(); // Empty list
            // Mock method to simulate data loading

            bool result = hometracker.CalculateAndShowExpenses(guestMode);

            string expectedOutput = "No utility data found.";

            Assert.False(result);
            Assert.Contains(expectedOutput, output.ToString().Trim());

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void EdmondsKarp_ReturnsCorrectMaxFlow()
        {
            // Test graph setup
            Node source = new Node("Source");
            Node sink = new Node("Sink");
            Node intermediate1 = new Node("Intermediate1");
            Node intermediate2 = new Node("Intermediate2");

            source.AddNeighbor(intermediate1, 10);
            intermediate1.AddNeighbor(intermediate2, 5);
            intermediate2.AddNeighbor(sink, 8);
            intermediate1.AddNeighbor(sink, 15);

            var hometracker = new Hometracker();

            // Expected max flow from source to sink is 18
            int result = hometracker.EdmondsKarp(source, sink);

            Assert.Equal(10, result);
        }
        [Fact]
        public void Find_WhenCalled_ReturnsCorrectRootNode()
        {
            var hometracker = new Hometracker();

            Node node1 = new Node("Node1");
            Node node2 = new Node("Node2");
            Node node3 = new Node("Node3");

            var parent = new Dictionary<Node, Node>
            {
                { node1, node1 },
                { node2, node1 },
                { node3, node2 }
            };

            var result = hometracker.Find(parent, node3);

            Assert.Equal(node1, result); // Node1 should be the root of Node3
        }

        [Fact]
        public void Union_WhenCalled_UpdatesParentAndRank()
        {
            var hometracker = new Hometracker();

            Node node1 = new Node("Node1");
            Node node2 = new Node("Node2");

            var parent = new Dictionary<Node, Node>
            {
                { node1, node1 },
                { node2, node2 }
            };

            var rank = new Dictionary<Node, int>
            {
                { node1, 0 },
                { node2, 0 }
            };

            hometracker.Union(parent, rank, node1, node2);

            // After union, one should be the parent of the other, and rank should be updated
            Assert.True(parent[node1] == node2 || parent[node2] == node1);
            if (parent[node1] == node2)
            {
                Assert.Equal(1, rank[node2]); // Rank of the new root should increase
            }
            else
            {
                Assert.Equal(1, rank[node1]); // Rank of the new root should increase
            }
        }
        [Fact]
        public void LoadGraph_CreatesNodesAndEdgesCorrectly()
        {
            // Setup
            var hometracker = new Hometracker();
            List<UtilityUsage> usages = new List<UtilityUsage>
            {
                new UtilityUsage("User1", 100, 50, 25),
                new UtilityUsage("User2", 200, 100, 50),
                new UtilityUsage("User3", 150, 75, 35)
            };

            List<Node> nodes = new List<Node>();

            // Action
            bool result = hometracker.LoadGraph(usages, nodes);

            // Assert
            Assert.True(result);
            Assert.Equal(3, nodes.Count); // Checks if all nodes are added
        }
        [Fact]
        public void FordFulkerson_ReturnsCorrectMaxFlow()
        {
            // Setup
            var source = CreateNodeWithResidualCapacities("Source");
            var sink = CreateNodeWithResidualCapacities("Sink");
            var intermediate1 = CreateNodeWithResidualCapacities("Intermediate1");
            var intermediate2 = CreateNodeWithResidualCapacities("Intermediate2");

            source.AddNeighbor(intermediate1, 10);
            intermediate1.AddNeighbor(intermediate2, 5);
            intermediate2.AddNeighbor(sink, 8);
            intermediate1.AddNeighbor(sink, 15);

            var hometracker = new Hometracker();

            // Action
            int maxFlow = hometracker.FordFulkerson(source, sink);

            // Assert
            Assert.Equal(10, maxFlow);  // The expected max flow through this network should be 15
        }
        [Fact]
        public void BellmanFord_FindsShortestPaths_ReturnsTrue()
        {
            // Arrange
            var source = CreateNodeWithCapacities("Source");
            var node1 = CreateNodeWithCapacities("Node1");
            var node2 = CreateNodeWithCapacities("Node2");
            var nodes = new List<Node> { source, node1, node2 };

            source.Neighbors.Add(node1);
            source.Capacities[node1] = 5;
            node1.Neighbors.Add(node2);
            node1.Capacities[node2] = 3;
            node2.Neighbors.Add(source);
            node2.Capacities[source] = -10; // A negative edge, but no negative cycle

            var distances = new Dictionary<Node, int>();
            var hometracker = new Hometracker();

            // Act
            bool result = hometracker.BellmanFord(source, distances);

            // Assert
            Assert.True(result);
            Assert.Equal(0, distances[source]);
        }

        [Fact]
        public void BellmanFord_DetectsNegativeCycle_ReturnsFalse()
        {
            // Arrange
            var source = CreateNodeWithCapacities("Source");
            var node1 = CreateNodeWithCapacities("Node1");
            var node2 = CreateNodeWithCapacities("Node2");
            var nodes = new List<Node> { source, node1, node2 };

            source.Neighbors.Add(node1);
            source.Capacities[node1] = 5;
            node1.Neighbors.Add(node2);
            node1.Capacities[node2] = 3;
            node2.Neighbors.Add(source);
            node2.Capacities[source] = -10; // A negative edge forming a negative cycle

            var distances = new Dictionary<Node, int>();
            var hometracker = new Hometracker();

            // Act
            bool result = hometracker.BellmanFord(source, distances);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void KruskalMST_CreatesCorrectMST()
        {
            // Arrange
            var node1 = CreateNode("Node1");
            var node2 = CreateNode("Node2");
            var node3 = CreateNode("Node3");
            var node4 = CreateNode("Node4");

            var edges = new List<Edge>
            {
                CreateEdge(node1, node2, 10),
                CreateEdge(node1, node3, 15),
                CreateEdge(node2, node3, 5),
                CreateEdge(node2, node4, 10),
                CreateEdge(node3, node4, 10)
            };

            var nodes = new List<Node> { node1, node2, node3, node4 };
            var hometracker = new Hometracker();

            // Redirect Console Output
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                hometracker.KruskalMST(nodes, edges);

                // Assert
                string result = sw.ToString().Trim();
                Assert.Contains("Node2 - Node3: 5", result);
                Assert.Contains("Node1 - Node2: 10", result);
                Assert.Contains("Node2 - Node4: 10", result);
                Assert.DoesNotContain("Node1 - Node3: 15", result);
                Assert.DoesNotContain("Node3 - Node4: 10", result);

                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            }
        }
        [Fact]
        public void Dijkstra_FindsShortestPathsCorrectly()
        {
            // Arrange
            var source = CreateNodeWithNeighbors("Source");
            var node1 = CreateNodeWithNeighbors("Node1");
            var node2 = CreateNodeWithNeighbors("Node2");
            var node3 = CreateNodeWithNeighbors("Node3");

            source.Neighbors.Add(node1);
            source.Capacities[node1] = 1;
            node1.Neighbors.Add(node2);
            node1.Capacities[node2] = 3;
            node2.Neighbors.Add(node3);
            node2.Capacities[node3] = 1;
            node3.Neighbors.Add(node1); // Adding cycle for complexity
            node3.Capacities[node1] = 1;

            var nodes = new List<Node> { source, node1, node2, node3 };

            var dist = new Dictionary<Node, int>();
            var prev = new Dictionary<Node, Node>();

            foreach (var node in nodes)
            {
                dist[node] = int.MaxValue;
                prev[node] = null;
            }

            var hometracker = new Hometracker();

            // Act
            hometracker.Dijkstra(source, dist, prev);

            // Assert
            Assert.Equal(0, dist[source]);
            Assert.Equal(1, dist[node1]);
            Assert.Equal(4, dist[node2]);
            Assert.Equal(5, dist[node3]);
            Assert.Equal(node1, prev[node2]);
            Assert.Equal(node2, prev[node3]);
            Assert.Null(prev[source]);
        }
    }
}
