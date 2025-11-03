/**
 * Хелпер для работы с Chart.js
 */
window.chartHelper = {
    chart: null,

    /**
     * Обновление или создание диаграммы
     * @param {string[]} labels - Метки (символы)
     * @param {number[]} data - Значения (количество)
     */
    updateChart: function (labels, data) {
        const ctx = document.getElementById('frequencyChart');
        if (!ctx) {
            console.error('Canvas element not found');
            return;
        }

        // Уничтожаем старую диаграмму если существует
        if (this.chart) {
            this.chart.destroy();
        }

        // Создаем новую диаграмму
        this.chart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Количество повторений',
                    data: data,
                    backgroundColor: 'rgba(76, 175, 80, 0.6)',
                    borderColor: 'rgba(76, 175, 80, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                indexAxis: 'y', // Горизонтальный bar chart
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: false
                    },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                const value = context.parsed.x;
                                const total = context.dataset.data.reduce((a, b) => a + b, 0);
                                const percentage = ((value / total) * 100).toFixed(2);
                                return [
                                    `Количество: ${value.toLocaleString()}`,
                                    `Процент: ${percentage}%`
                                ];
                            }
                        }
                    }
                },
                scales: {
                    x: {
                        beginAtZero: true,
                        ticks: {
                            callback: function (value) {
                                return value.toLocaleString();
                            }
                        }
                    }
                }
            }
        });
    },

    /**
     * Уничтожение диаграммы
     */
    destroyChart: function () {
        if (this.chart) {
            this.chart.destroy();
            this.chart = null;
        }
    }
};