using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ParticlePhysicsSimulator
{
    public partial class MainForm : Form
    {
        private List<Particle> particles;
        private System.Windows.Forms.Timer simulationTimer;
        private Random random;
        private bool isSimulationRunning;
        private Point mousePosition;
        private bool isMousePressed;
        
        // UI Controls
        private Panel canvasPanel = null!;
        private Button startStopButton = null!;
        private Button clearButton = null!;
        private Button addParticleButton = null!;
        private TrackBar gravityTrackBar = null!;
        private TrackBar dampingTrackBar = null!;
        private Label gravityLabel = null!;
        private Label dampingLabel = null!;
        private ComboBox particleTypeCombo = null!;
        private Label particleCountLabel = null!;
        private CheckBox enableGravityCheckBox = null!;
        private CheckBox enableCollisionCheckBox = null!;

        public MainForm()
        {
            InitializeComponent();
            particles = new List<Particle>();
            random = new Random();
            isSimulationRunning = false;
            
            simulationTimer = new System.Windows.Forms.Timer();
            simulationTimer.Interval = 16; // ~60 FPS
            simulationTimer.Tick += SimulationTimer_Tick;
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1000, 700);
            this.Text = "Particle Physics Simulator";
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Canvas Panel
            canvasPanel = new Panel();
            canvasPanel.Location = new Point(10, 10);
            canvasPanel.Size = new Size(700, 500);
            canvasPanel.BackColor = Color.Black;
            canvasPanel.BorderStyle = BorderStyle.FixedSingle;
            canvasPanel.Paint += CanvasPanel_Paint;
            canvasPanel.MouseDown += CanvasPanel_MouseDown;
            canvasPanel.MouseUp += CanvasPanel_MouseUp;
            canvasPanel.MouseMove += CanvasPanel_MouseMove;
            this.Controls.Add(canvasPanel);

            // Control Panel
            int controlX = 730;
            int currentY = 20;

            // Start/Stop Button
            startStopButton = new Button();
            startStopButton.Location = new Point(controlX, currentY);
            startStopButton.Size = new Size(100, 30);
            startStopButton.Text = "Start";
            startStopButton.BackColor = Color.FromArgb(0, 122, 204);
            startStopButton.ForeColor = Color.White;
            startStopButton.FlatStyle = FlatStyle.Flat;
            startStopButton.Click += StartStopButton_Click;
            this.Controls.Add(startStopButton);

            currentY += 40;

            // Clear Button
            clearButton = new Button();
            clearButton.Location = new Point(controlX, currentY);
            clearButton.Size = new Size(100, 30);
            clearButton.Text = "Clear";
            clearButton.BackColor = Color.FromArgb(204, 0, 0);
            clearButton.ForeColor = Color.White;
            clearButton.FlatStyle = FlatStyle.Flat;
            clearButton.Click += ClearButton_Click;
            this.Controls.Add(clearButton);

            currentY += 50;

            // Particle Type
            Label particleTypeLabel = new Label();
            particleTypeLabel.Location = new Point(controlX, currentY);
            particleTypeLabel.Size = new Size(100, 20);
            particleTypeLabel.Text = "Particle Type:";
            particleTypeLabel.ForeColor = Color.White;
            this.Controls.Add(particleTypeLabel);

            currentY += 25;

            particleTypeCombo = new ComboBox();
            particleTypeCombo.Location = new Point(controlX, currentY);
            particleTypeCombo.Size = new Size(120, 25);
            particleTypeCombo.Items.AddRange(new string[] { "Normal", "Heavy", "Light", "Bouncy" });
            particleTypeCombo.SelectedIndex = 0;
            this.Controls.Add(particleTypeCombo);

            currentY += 40;

            // Add Particle Button
            addParticleButton = new Button();
            addParticleButton.Location = new Point(controlX, currentY);
            addParticleButton.Size = new Size(100, 30);
            addParticleButton.Text = "Add Particle";
            addParticleButton.BackColor = Color.FromArgb(0, 153, 76);
            addParticleButton.ForeColor = Color.White;
            addParticleButton.FlatStyle = FlatStyle.Flat;
            addParticleButton.Click += AddParticleButton_Click;
            this.Controls.Add(addParticleButton);

            currentY += 50;

            // Gravity Controls
            gravityLabel = new Label();
            gravityLabel.Location = new Point(controlX, currentY);
            gravityLabel.Size = new Size(120, 20);
            gravityLabel.Text = "Gravity: 0.5";
            gravityLabel.ForeColor = Color.White;
            this.Controls.Add(gravityLabel);

            currentY += 25;

            gravityTrackBar = new TrackBar();
            gravityTrackBar.Location = new Point(controlX, currentY);
            gravityTrackBar.Size = new Size(120, 45);
            gravityTrackBar.Minimum = 0;
            gravityTrackBar.Maximum = 20;
            gravityTrackBar.Value = 5;
            gravityTrackBar.TickFrequency = 5;
            gravityTrackBar.ValueChanged += GravityTrackBar_ValueChanged;
            this.Controls.Add(gravityTrackBar);

            currentY += 55;

            // Damping Controls
            dampingLabel = new Label();
            dampingLabel.Location = new Point(controlX, currentY);
            dampingLabel.Size = new Size(120, 20);
            dampingLabel.Text = "Damping: 0.99";
            dampingLabel.ForeColor = Color.White;
            this.Controls.Add(dampingLabel);

            currentY += 25;

            dampingTrackBar = new TrackBar();
            dampingTrackBar.Location = new Point(controlX, currentY);
            dampingTrackBar.Size = new Size(120, 45);
            dampingTrackBar.Minimum = 90;
            dampingTrackBar.Maximum = 100;
            dampingTrackBar.Value = 99;
            dampingTrackBar.TickFrequency = 2;
            dampingTrackBar.ValueChanged += DampingTrackBar_ValueChanged;
            this.Controls.Add(dampingTrackBar);

            currentY += 55;

            // Enable Gravity Checkbox
            enableGravityCheckBox = new CheckBox();
            enableGravityCheckBox.Location = new Point(controlX, currentY);
            enableGravityCheckBox.Size = new Size(120, 20);
            enableGravityCheckBox.Text = "Enable Gravity";
            enableGravityCheckBox.ForeColor = Color.White;
            enableGravityCheckBox.Checked = true;
            this.Controls.Add(enableGravityCheckBox);

            currentY += 30;

            // Enable Collision Checkbox
            enableCollisionCheckBox = new CheckBox();
            enableCollisionCheckBox.Location = new Point(controlX, currentY);
            enableCollisionCheckBox.Size = new Size(120, 20);
            enableCollisionCheckBox.Text = "Enable Collisions";
            enableCollisionCheckBox.ForeColor = Color.White;
            enableCollisionCheckBox.Checked = true;
            this.Controls.Add(enableCollisionCheckBox);

            currentY += 40;

            // Particle Count Label
            particleCountLabel = new Label();
            particleCountLabel.Location = new Point(controlX, currentY);
            particleCountLabel.Size = new Size(120, 20);
            particleCountLabel.Text = "Particles: 0";
            particleCountLabel.ForeColor = Color.White;
            this.Controls.Add(particleCountLabel);

            // Instructions
            currentY += 40;
            Label instructionsLabel = new Label();
            instructionsLabel.Location = new Point(controlX, currentY);
            instructionsLabel.Size = new Size(200, 80);
            instructionsLabel.Text = "Instructions:\n• Click to add particles\n• Drag to create multiple\n• Adjust physics settings\n• Watch particles interact!";
            instructionsLabel.ForeColor = Color.LightGray;
            instructionsLabel.Font = new Font("Segoe UI", 8);
            this.Controls.Add(instructionsLabel);
        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if (isSimulationRunning)
            {
                simulationTimer.Stop();
                startStopButton.Text = "Start";
                startStopButton.BackColor = Color.FromArgb(0, 122, 204);
            }
            else
            {
                simulationTimer.Start();
                startStopButton.Text = "Stop";
                startStopButton.BackColor = Color.FromArgb(204, 122, 0);
            }
            isSimulationRunning = !isSimulationRunning;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            particles.Clear();
            UpdateParticleCount();
            canvasPanel.Invalidate();
        }

        private void AddParticleButton_Click(object sender, EventArgs e)
        {
            AddRandomParticle();
        }

        private void GravityTrackBar_ValueChanged(object sender, EventArgs e)
        {
            float gravity = gravityTrackBar.Value / 10.0f;
            gravityLabel.Text = $"Gravity: {gravity:F1}";
        }

        private void DampingTrackBar_ValueChanged(object sender, EventArgs e)
        {
            float damping = dampingTrackBar.Value / 100.0f;
            dampingLabel.Text = $"Damping: {damping:F2}";
        }

        private void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isMousePressed = true;
            mousePosition = e.Location;
            AddParticleAtPosition(e.Location);
        }

        private void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;
        }

        private void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMousePressed)
            {
                AddParticleAtPosition(e.Location);
            }
            mousePosition = e.Location;
        }

        private void AddParticleAtPosition(Point position)
        {
            ParticleType type = (ParticleType)particleTypeCombo.SelectedIndex;
            Particle particle = new Particle(position.X, position.Y, type, random);
            particles.Add(particle);
            UpdateParticleCount();
        }

        private void AddRandomParticle()
        {
            int x = random.Next(50, canvasPanel.Width - 50);
            int y = random.Next(50, canvasPanel.Height - 50);
            ParticleType type = (ParticleType)particleTypeCombo.SelectedIndex;
            Particle particle = new Particle(x, y, type, random);
            particles.Add(particle);
            UpdateParticleCount();
        }

        private void UpdateParticleCount()
        {
            particleCountLabel.Text = $"Particles: {particles.Count}";
        }

        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            UpdatePhysics();
            canvasPanel.Invalidate();
        }

        private void UpdatePhysics()
        {
            float gravity = gravityTrackBar.Value / 10.0f;
            float damping = dampingTrackBar.Value / 100.0f;

            foreach (var particle in particles)
            {
                // Apply gravity
                if (enableGravityCheckBox.Checked)
                {
                    particle.VelocityY += gravity;
                }

                // Update position
                particle.X += particle.VelocityX;
                particle.Y += particle.VelocityY;

                // Apply damping
                particle.VelocityX *= damping;
                particle.VelocityY *= damping;

                // Boundary collisions
                if (particle.X <= particle.Radius || particle.X >= canvasPanel.Width - particle.Radius)
                {
                    particle.VelocityX *= -particle.Bounciness;
                    particle.X = Math.Max(particle.Radius, Math.Min(canvasPanel.Width - particle.Radius, particle.X));
                }

                if (particle.Y <= particle.Radius || particle.Y >= canvasPanel.Height - particle.Radius)
                {
                    particle.VelocityY *= -particle.Bounciness;
                    particle.Y = Math.Max(particle.Radius, Math.Min(canvasPanel.Height - particle.Radius, particle.Y));
                }
            }

            // Particle collisions
            if (enableCollisionCheckBox.Checked)
            {
                for (int i = 0; i < particles.Count; i++)
                {
                    for (int j = i + 1; j < particles.Count; j++)
                    {
                        CheckCollision(particles[i], particles[j]);
                    }
                }
            }
        }

        private void CheckCollision(Particle p1, Particle p2)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            float minDistance = p1.Radius + p2.Radius;

            if (distance < minDistance && distance > 0)
            {
                // Normalize collision vector
                dx /= distance;
                dy /= distance;

                // Separate particles
                float overlap = minDistance - distance;
                float separationX = dx * overlap * 0.5f;
                float separationY = dy * overlap * 0.5f;

                p1.X -= separationX;
                p1.Y -= separationY;
                p2.X += separationX;
                p2.Y += separationY;

                // Calculate relative velocity
                float relativeVelX = p2.VelocityX - p1.VelocityX;
                float relativeVelY = p2.VelocityY - p1.VelocityY;

                // Calculate collision impulse
                float collisionSpeed = relativeVelX * dx + relativeVelY * dy;
                
                if (collisionSpeed > 0) return; // Objects separating

                float restitution = (p1.Bounciness + p2.Bounciness) * 0.5f;
                float impulse = -(1 + restitution) * collisionSpeed / (1/p1.Mass + 1/p2.Mass);

                // Apply impulse
                float impulseX = impulse * dx;
                float impulseY = impulse * dy;

                p1.VelocityX -= impulseX / p1.Mass;
                p1.VelocityY -= impulseY / p1.Mass;
                p2.VelocityX += impulseX / p2.Mass;
                p2.VelocityY += impulseY / p2.Mass;
            }
        }

        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw particles
            foreach (var particle in particles)
            {
                using (Brush brush = new SolidBrush(particle.Color))
                {
                    float diameter = particle.Radius * 2;
                    g.FillEllipse(brush, particle.X - particle.Radius, particle.Y - particle.Radius, diameter, diameter);
                }

                // Draw velocity vector (optional)
                if (Math.Abs(particle.VelocityX) > 0.1f || Math.Abs(particle.VelocityY) > 0.1f)
                {
                    using (Pen pen = new Pen(Color.Yellow, 1))
                    {
                        g.DrawLine(pen, particle.X, particle.Y, 
                                  particle.X + particle.VelocityX * 5, 
                                  particle.Y + particle.VelocityY * 5);
                    }
                }
            }

            // Draw mouse position when creating particles
            if (isMousePressed)
            {
                using (Pen pen = new Pen(Color.White, 2))
                {
                    g.DrawEllipse(pen, mousePosition.X - 10, mousePosition.Y - 10, 20, 20);
                }
            }
        }
    }

    public enum ParticleType
    {
        Normal,
        Heavy,
        Light,
        Bouncy
    }

    public class Particle
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public float Mass { get; set; }
        public float Radius { get; set; }
        public Color Color { get; set; }
        public float Bounciness { get; set; }

        public Particle(float x, float y, ParticleType type, Random random)
        {
            X = x;
            Y = y;
            
            // Random initial velocity
            VelocityX = (float)(random.NextDouble() - 0.5) * 4;
            VelocityY = (float)(random.NextDouble() - 0.5) * 4;

            SetParticleProperties(type, random);
        }

        private void SetParticleProperties(ParticleType type, Random random)
        {
            switch (type)
            {
                case ParticleType.Normal:
                    Mass = 1.0f;
                    Radius = 8;
                    Color = Color.FromArgb(100, 150, 255);
                    Bounciness = 0.7f;
                    break;

                case ParticleType.Heavy:
                    Mass = 3.0f;
                    Radius = 12;
                    Color = Color.FromArgb(255, 100, 100);
                    Bounciness = 0.5f;
                    break;

                case ParticleType.Light:
                    Mass = 0.3f;
                    Radius = 5;
                    Color = Color.FromArgb(100, 255, 150);
                    Bounciness = 0.9f;
                    break;

                case ParticleType.Bouncy:
                    Mass = 0.8f;
                    Radius = 10;
                    Color = Color.FromArgb(255, 200, 100);
                    Bounciness = 1.2f;
                    break;
            }

            // Add some color variation
            int colorVariation = random.Next(-30, 31);
            int r = Math.Max(0, Math.Min(255, Color.R + colorVariation));
            int g = Math.Max(0, Math.Min(255, Color.G + colorVariation));
            int b = Math.Max(0, Math.Min(255, Color.B + colorVariation));
            Color = Color.FromArgb(r, g, b);
        }
    }
}