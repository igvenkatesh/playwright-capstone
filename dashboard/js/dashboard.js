async function loadResults() {
  try {
    const response = await fetch('data/results.json');
    const results = await response.json();

    const tableBody = document.getElementById('results-table');
    const suites = results.suites || [];

    if (suites.length === 0) {
      const row = document.createElement('tr');
      row.innerHTML = `
        <td>All Suites</td>
        <td>${results.stats?.expected || 0}</td>
        <td>${results.stats?.unexpected || 0}</td>
      `;
      tableBody.appendChild(row);
      return;
    }

    const labels = [];
    const passedData = [];
    const failedData = [];

    suites.forEach(suite => {
      (suite.specs || []).forEach(spec => {
        const tests = spec.tests || [];

        let passed = 0;
        let failed = 0;

        tests.forEach(test => {
          (test.results || []).forEach(r => {
            if (r.status === 'passed') {
              passed++;
            } else {
              failed++;
            }
          });
        });

        // Combine project name with suite/spec title
        const projectName = tests[0]?.projectName || 'Unknown Project';
        const combinedTitle = `${projectName} - ${suite.title} - ${spec.title}`;

        const row = document.createElement('tr');
        row.innerHTML = `
          <td>${combinedTitle}</td>
          <td>${passed}</td>
          <td>${failed}</td>
        `;
        tableBody.appendChild(row);

        labels.push(combinedTitle);
        passedData.push(passed);
        failedData.push(failed);
      });
    });

    // Render chart
    const ctx = document.getElementById('resultsChart').getContext('2d');
    new Chart(ctx, {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [
          {
            label: 'Passed',
            data: passedData,
            backgroundColor: 'rgba(75, 192, 192, 0.7)'
          },
          {
            label: 'Failed',
            data: failedData,
            backgroundColor: 'rgba(255, 99, 132, 0.7)'
          }
        ]
      },
      options: {
        responsive: true,
        plugins: {
          title: {
            display: true,
            text: 'Playwright Test Results by Project & Suite'
          }
        }
      }
    });
  } catch (err) {
    console.error('Error loading results.json:', err);
  }
}

loadResults();
